param (
    [string]$filePath = "InstallerProject.vdproj",
    [string]$newVersion = "1.2.3",
    [string]$newProductName = "Sheet List Inventor Add-in"
)

# Read the content of the .vdproj file
if (!(Test-Path $filePath)) {
    Write-Error "File not found: $filePath"
    exit 1
}

$content = Get-Content -Path $filePath

# Update the 'ProductVersion' and 'ProductName' using regex
$content = $content -replace '("ProductVersion" = "8:)([^"]*)(")', "`$1$newVersion`$3"
$content = $content -replace '("ProductName" = "8:)([^"]*)(")', "`$1$newProductName`$3"

# Write the updated content back to the file
Set-Content -Path $filePath -Value $content -Encoding UTF8

Write-Output "Updated ProductVersion to $newVersion and ProductName to $newProductName"