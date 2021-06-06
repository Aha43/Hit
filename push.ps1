$wdir = Get-Item .
Write-Host 
Write-Host ("Starting push for '" + $wdir.Name + "'") -ForegroundColor Yellow
Write-Host

[string]$gitStatus = (git status --porcelain)
if ($gitStatus) {
    Write-Error "Commit work before pushing!" -ErrorAction Stop
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
    exit
}

Write-Host
Write-Host 'Running tests...' -ForegroundColor Yellow
$testResult = (dotnet test -v normal *>&1) -join [System.Environment]::NewLine
Write-Host $testResult
if ($LASTEXITCODE -eq 0) {
    Write-Host "Tests ok!" -ForegroundColor Green
}
else {
    Write-Host
    Write-Host 'NO GO: Tests failed!' -ForegroundColor Magenta
    Write-Host
    exit
}

Write-Host
Write-Host 'Doing the push' -ForegroundColor Yellow
git push
