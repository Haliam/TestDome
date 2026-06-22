$lib = Get-ChildItem -Path 'd:\000 Repos\000 Training\TestDome\src\TestDome.Library' -Filter '*.cs' | Select-Object -ExpandProperty BaseName
$docs = Get-ChildItem -Path 'd:\000 Repos\000 Training\TestDome\docs\Exercises' -Filter '*.md' | Select-Object -ExpandProperty BaseName
Compare-Object -ReferenceObject $lib -DifferenceObject $docs | Where-Object { $_.SideIndicator -eq '<=' } | Select-Object -ExpandProperty InputObject | Out-File 'd:\000 Repos\000 Training\TestDome\tools\missing.txt' -Encoding UTF8
Write-Output 'Done'