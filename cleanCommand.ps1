Get-ChildItem .\ -include bin,obj -Recurse | foreach ($_) { remove-item $_.fullname -Force -Recurse }
Compress-Archive . ../$(Split-Path -Path (Get-Location) -Leaf) -force
ii ../