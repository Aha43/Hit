$go = $args[0]

[string]$gitStatus = (git status --porcelain)
if ($gitStatus) {
    Write-Host
    Write-Host 'Preparing a release...'
}
else {
    Write-Error 'Nothing to commit as a release!' -ErrorAction Stop
}

Write-Host
Write-Host 'Doing a git pull...'
git pull

Write-Host
Write-Host 'Doing a build'

Write-Host
Write-Host 'Running tests'

$releaseFile = Get-ChildItem -Path '.\release.xml'
if ($releaseFile) {
    [XML]$relaseXml = Get-Content $releaseFile

    $devProjectFile = Get-ChildItem -Path ($relaseXml.release.devProjectFile)

    if ($devProjectFile) {
        [XML]$devXml = Get-Content $devProjectFile
        [string]$devVersion = $devXml.Project.PropertyGroup.Version

        [XML]$repoXml = (New-Object System.Net.WebClient).DownloadString($relaseXml.release.repoProjectFileUri)
        [string]$repoVersion = $repoXml.Project.PropertyGroup.Version

        $devVersion = '7.2.1-alpha' # sim hack

        Write-Host
        Write-Host ("Previous release is '" + $repoVersion + "'");
        Write-Host ("New release is '" + $devVersion + "'");
        
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
                Write-Host ("Will do release with the following commit message if given the go signal: '" + $commitMessage + "'")
                if ($go -and ($go -eq 'GO')) {
                    Write-Host
                    Write-Host ("GO signal given... doing release '" + $devVersion + "'")
                }
                else {
                    Write-Host
                    Write-Host ("GO signal NOT given... '" + $devVersion + "' NOT released")
                }
                # Modify [CmdletBinding()] to [CmdletBinding(SupportsShouldProcess=$true, DefaultParameterSetName='Path')]
                $paths = @()
                if ($psCmdlet.ParameterSetName -eq 'Path') {
                    foreach ($aPath in $Path) {
                        if (!(Test-Path -Path $aPath)) {
                            $ex = New-Object System.Management.Automation.ItemNotFoundException "Cannot find path '$aPath' because it does not exist."
                            $category = [System.Management.Automation.ErrorCategory]::ObjectNotFound
                            $errRecord = New-Object System.Management.Automation.ErrorRecord $ex,'PathNotFound',$category,$aPath
                            $psCmdlet.WriteError($errRecord)
                            continue
                        }
                    
                        # Resolve any wildcards that might be in the path
                        $provider = $null
                        $paths += $psCmdlet.SessionState.Path.GetResolvedProviderPathFromPSPath($aPath, [ref]$provider)
                    }
                }
                else {
                    foreach ($aPath in $LiteralPath) {
                        if (!(Test-Path -LiteralPath $aPath)) {
                            $ex = New-Object System.Management.Automation.ItemNotFoundException "Cannot find path '$aPath' because it does not exist."
                            $category = [System.Management.Automation.ErrorCategory]::ObjectNotFound
                            $errRecord = New-Object System.Management.Automation.ErrorRecord $ex,'PathNotFound',$category,$aPath
                            $psCmdlet.WriteError($errRecord)
                            continue
                        }
                    
                        # Resolve any relative paths
                        $paths += $psCmdlet.SessionState.Path.GetUnresolvedProviderPathFromPSPath($aPath)
                    }
                }
                
                foreach ($aPath in $paths) {
                    if ($pscmdlet.ShouldProcess($aPath, 'Operation')) {
                        # Process each path
                        
                    }
                }
            }
            else {
                Write-Error 'Missing release issue'
            }
        }
        else {
            Write-Error ('Dev version (' + $devVersion + ') not greater than repo version (' + $repoVersion + ')')
        }
    }
    else {
        Write-Error 'Missing dev project file'
    }
}
else {
    Write-Error 'Missing release.xml in current dir'
}
Write-Host
