$go = $args[0]

$wdir = Get-Item .
Write-Host 
Write-Host ("Starting continous integration for '" + $wdir.Name + "'") -ForegroundColor Yellow
Write-Host

[string]$gitStatus = (git status --porcelain)
if ($gitStatus) {
    Write-Host
    Write-Host 'Preparing a release...' -ForegroundColor Yellow
}
else {
    Write-Error 'Nothing to commit as a release!' -ErrorAction Stop
}

Write-Host
Write-Host 'Doing a git pull...' -ForegroundColor Yellow
git pull

Write-Host
Write-Host 'Doing a build...' -ForegroundColor Yellow
$buildResult = (dotnet build *>&1)
if ($LASTEXITCODE -eq 0) {
    Write-Host "Build ok!" -ForegroundColor Green
}
else {
    Write-Host 
    $buildResult = $buildResult -join [System.Environment]::NewLine
    Write-Error $buildResult -ErrorAction Stop
    Write-Host
    Write-Host 'NO GO: Build failed!' -ForegroundColor Magenta
}

Write-Host
Write-Host 'Running tests...' -ForegroundColor Yellow
$testResult = (dotnet test *>&1)
if ($LASTEXITCODE -eq 0) {
    Write-Host "Tests ok!" -ForegroundColor Green
}
else {
    Write-Host 
    $testResult = $testResult -join [System.Environment]::NewLine
    Write-Error $testResult
    Write-Host
    Write-Host 'NO GO: Tests failed!' -ForegroundColor Magenta
    Write-Host
    exit
}

Write-Host
$releaseFile = Get-ChildItem -Path '.\release.xml'
if ($releaseFile) {
    [XML]$relaseXml = Get-Content $releaseFile

    $devProjectFile = Get-ChildItem -Path ($relaseXml.release.devProjectFile)

    if ($devProjectFile) {
        [XML]$devXml = Get-Content $devProjectFile
        [string]$devVersion = $devXml.Project.PropertyGroup.Version

        [string]$repoTxt = (New-Object System.Net.WebClient).DownloadString($relaseXml.release.repoProjectFileUri) 
        if (-not $repoTxt) {
            Write-Error 'Failed to read repo project file' -ErrorAction Stop
        }
        [XML]$repoXml = $repoTxt

        [string]$repoVersion = $repoXml.Project.PropertyGroup.Version

        Write-Host
        Write-Host ("Previous release is '" + $repoVersion + "'") -ForegroundColor Yellow
        Write-Host ("New release is '" + $devVersion + "'") -ForegroundColor Yellow
        
        if ($devVersion -gt $repoVersion)
        {
            [string]$releaseIssue = $relaseXml.release.releaseIssue
            if ($releaseIssue) {
                [string]$issues = ($relaseXml.release.issues.issue) -join ', #'

                [string]$commitMessage = ($devVersion  + ' (#' + $releaseIssue + ')')

                if ($issues) {
                    $commitMessage = ($commitMessage + ' -> (' + '#' + $issues + ')') 
                }

                Write-Host
                Write-Host ("Will do release with the following commit message if given the GO signal: '" + $commitMessage + "'") -ForegroundColor Yellow
                if ($go -and ($go -eq 'GO')) {
                    Write-Host
                    Write-Host ("GO signal given... doing release '" + $devVersion + "'") -ForegroundColor Yellow #tested
                    Write-Host
                    git add .
                    git commit -m $commitMessage
                }
                else {
                    Write-Host
                    Write-Host ("GO signal NOT given (this is a dry run)... '" + $devVersion + "' NOT released") -ForegroundColor Yellow #tested
                }
            }
            else {
                Write-Error 'Missing release issue' #tested
            }
        }
        else {
            Write-Error ('Dev version (' + $devVersion + ') not greater than repo version (' + $repoVersion + ')') #tested
        }
    }
    else {
        Write-Error 'Missing dev project file' #tested
    }
}
else {
    Write-Error 'Missing release.xml in current dir' #tested
}
Write-Host
