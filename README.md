# Clipboard
Text exctraction done the easy way

[![Build Status](https://dev.azure.com/matthewhope396/Clipboard/_apis/build/status/clipboard-org.clipboard?branchName=master)](https://dev.azure.com/matthewhope396/Clipboard/_build/latest?definitionId=4&branchName=master)

## Getting started
### Referencing the package
To start using cliboard you need to install the nuget package, which can be found [here](https://www.nuget.org/packages/Clipboard/)

Alternatively you can intall the package by executing: `Install-Package Clipboard`

### Extracting text
Extracting text is extremely simple using clipboard, you just need to supply a file path or filstream to the `TextExtractor.Open` method.

### Example
```c#
// Supply a path or filestream to a supported file
using(var exractor = TextExtractor.Open("example.docx")) 
{
    // Call the Extract method for a non async execution
    var text = extractor.Extract();

    // Call the ExtractAsync method to execute asynchronosly
    var text = await extractor.ExtractAsync();
}
```

## Supported file types

| Type       | Name                                      |
|------------|-------------------------------------------|
| Excel      | xlsm, .xlsx, .xltm, .xltx                 |
| Pdf        | .pdf                                      |
| Powerpoint | .potm, .potx, .ppsm, .ppsx, .pptm, .pptx  |
| Text       | .csv, .html, .txt, .xml                   |
| Word       | .docx, .dotm, .dotx                       |
