function Test-XmlBySchema
{
    [CmdletBinding()]
    [OutputType([bool])]
    param
    (
        [Parameter(Mandatory)]
        [ValidateScript({ Test-Path -Path $_ })]
        [ValidatePattern('\.xml')]
        [string]$XmlFile,
        [Parameter(Mandatory)]
        [ValidateScript({ Test-Path -Path $_ })]
        [ValidatePattern('\.xsd')]
        [string]$SchemaPath
    )

    try
    {
        [xml]$xml = Get-Content $XmlFile
        $xml.Schemas.Add('', $SchemaPath) | Out-Null
        $xml.Validate($null)
        Write-Verbose "Successfully validated $XmlFile against schema ($SchemaPath)"
        $result = $true
    }
    catch
    {
        $err = $_.Exception.Message
        Write-Verbose "Failed to validate $XmlFile against schema ($SchemaPath)`nDetails: $err"
        $result = $false
    }
    finally
    {
        $result
    }
}

$schemaPathSrc = "nhibernate-mapping.xsd"
$xmlFileSrc = $args[0]
Test-XmlBySchema -XmlFile $xmlFileSrc -SchemaPath $schemaPathSrc -Verbose
