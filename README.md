# Html Tools 2019

[![Build status](https://ci.appveyor.com/api/projects/status/c7w5c0kjva6jv0yu?svg=true)](https://ci.appveyor.com/project/madskristensen/htmltools2019)

Download this extension from the [VS Marketplace](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.HtmlTools)
or get the [CI build](http://vsixgallery.com/extension/8682fae0-2ce9-49ea-93b5-edc92c118e2a/).

---------------------------------------

Productivity tools for the HTML editor

## Features

- Image hover preview
- Go to definition of classes and IDs
- Peek definition
- Meta tag Intellisense
- Dynamic Intellisense
- Find all references
- Light bulbs
- Validation

### Image hover preview
![Image hover](art/imagehover.png)

### Go to definition
Hit `F12` when the cursor is located inside a class or ID attribute. 
HTML Tools will jump to the location inside CSS, LESS and Sass files. 

### Peek definition
Hit `Alt+F12` when the curser is located inside a class or ID attribute.
A peek definition inline code window will appear showing the definition
of the class/ID from CSS, LESS and Sass files.

### Meta tags
Full Intellisense provided for: 
- Apple iOS
- Twitter Cards
- Facebook/OpenGraph
- Windows 8
- Internet Explorer 9+
- Viewport

### Dynamic Intellisense
Dynamic Intellisense is where Intellisense is based on other tags and attributes etc.
```html
<label for="here">
<input id="here" /> based on <label> tags
<datalist> IDs
```

![Dynamic Intellisense](art/dynamicintellisense.png)

### Find all references
Hit `Shift+F12` when the cursor is located inside a class attribute. 
HTML Tools will search all CSS, LESS and SASS files for the
class name. 

### Light bulbs
Light bulbs are added for variuos scenarios including:

- Base64 decoding of images
- Extract JavaScript to file 
- Remove parent tags 
- Extract Stylesheets to file 

### Validation

#### OpenGraph prefix
When working with Facebook/OpenGraph integration, we need to 
remember to add the prefix attribute to the `<head>` element. 

#### Microdata
Validates that the itemtype attribute is a valid absolute URL.

#### rel=logo
Validates that the `type` attribute has the value `image/svg`

## License
[Apache 2.0](LICENSE)