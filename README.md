![Clipboard](https://github.com/clipboard-org/clipboard/blob/master/src/Clipboard/images/icon.png)
# Clipboard
Text exctraction done the easy way

![Build](https://github.com/clipboard-org/clipboard/workflows/Build/badge.svg)

## Getting started
### Referencing the package
To start using cliboard you need to install the nuget package, which can be found [here](https://www.nuget.org/packages/Clipboard/)

Alternatively you can intall the package by executing: `Install-Package Clipboard`

### Extracting text
Extracting text is extremely simple using clipboard, you just need to supply a file path or filstream to the `TextExtractor.Open` method.

### Example
```c#
using(var exractor = TextExtractor.Open("example.pdf")) 
{
    // Call the Extract method for a non async execution
    var text = extractor.Extract();

    // Call the ExtractAsync method to execute asynchronosly
    var text = await extractor.ExtractAsync();
}

// There are many overloads for TextExtractor.Open

// You can pass in just the file path and a new FileStream will be created
TextExtractor.Open(string filepath)

// You can pass in your own FileStream
TextExtractor.Open(FileStream filestream)

// You can pass in a Stream and the content type wil be extracted from the bytes
TextExtractor.Open(Stream stream)

// You can pass in the bytes and the content type, content types can be accessed via the static `ContentType` class
TextExtractor.Open(byte[] bytes, string contentType)

// You can pass in a raw byte array and the content type wil be extracted
TextExtractor.Open(byte[] bytes)

// You can pass in a Readonly memory of bytes and the content type wil be extracted
TextExtractor.Open(ReadOnlyMemory<byte> bytes)
```

## Supported file types

| Type       | Name                                      |
|------------|-------------------------------------------|
| Excel      | xlsm, .xlsx, .xltm, .xltx                 |
| Pdf        | .pdf                                      |
| Powerpoint | .potm, .potx, .ppsm, .ppsx, .pptm, .pptx  |
| Text       | .csv, .html, .txt, .xml                   |
| Word       | .docx, .dotm, .dotx                       |
