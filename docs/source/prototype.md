# Prototype

## Introduction

```
-- let's go with image first --

[IMAGE logo]

-- then simple edit field --

[EDIT 'Name']

-- and button to go --

[BUTTON 'Go']
```

## Usage

### Simple prototyping

It is easy to write down cherry script using just a piece of paper.

### Code generation

Libraries in this project will help with generating UI layout in format specific for the platform. You may generate XML for Android as well as HTML or even builder code for GUI toolkit you need.

### Embedded systems

Cherry script may be converted to bytecode which will result in tiny amount of data needed to store or transmit over network.

## Definition

Definition of interface appearance consists of a list of elements, also called components, together with attributes assigned to them such as name, text or other properties.

Each element is of a specific kind and this type is written in square brackets together with the attributes. For example button is represented simply by ``[BUTTON]``.

Attributes are defined as a set of *key*=*value* pairs. If a value contain any special characters or spaces, it should be surrounded with citation marks.

When you're insane you might also write something like ``['BUTTON' "name"=`value`]`` but as you can see, it doesn't look well.

Every text outside square brackets is ignored. One line ``//`` and multiline ``/*`` ``*/`` comments are allowed.

```
[TEXT text='Hello, world.']

//[TEXT text='This element is commented out']

This is ignored static text.

/*
 * Inside multiline comments you may also use
 * [TEXT text='Nothing'] and it will be ignored too.
 */

[BUTTON text='Thank you']
```

Component names and attribute keys are considered to be case insensitive. 
For nicer look component names are usually written with capital letters. Attribute key names are also case insensitive but it is recommended to use lower case letters for them. And finally it is nice to write predefined values in capital letters.

Values that need to be surrounded by quotation marks might be written using apostrophes (which is recommended) or double quotes.

Elements are packed into groups. Each group defines vertical (default) or horizontal layout.

### Escaping

When in need to use of quotation character inside value content, doublets might be used like in folllowing examples.

```
[LABEL text='That''s my piece of floor']
```

## Set

Root components.

| Element      |     | Description               |
|--------------|:---:|---------------------------|
| [SCREEN]     | [S] | Screen definition.        |
| [FRAGMENT]   | [F] | Fragment definition.      |
| [USE]        | [U] | Placeholder for fragment. |

Layout components.

| Element      |     | Description     |
|--------------|:---:|-----------------|
| [GROUP]      | [G] | Layout group.   |
| [PAGE]       | [P] | Page tab.       |

Basic components.

| Element      |     | Description     |
|--------------|:---:|-----------------|
| [TEXT]       | [T] | Static text.    |
| [EDIT]       | [E] | Edit field.     |
| [CHECK]      | [C] | Check button.   |
| [RADIO]      | [R] | Radio button.   |
| [IMAGE]      | [I] | Graphics image. |
| [LINK]       | [L] | Link field.     |
| [DATA]       | [D] | Data view.      |
| [MENU]       | [M] | Menu item.      |

## Unicode

For values there is full unicode support.

```
[TEXT text='❤️']
```

```
[TEXT 💩]
```

## Sticking

Elements are sticked to previous elements and there is no need to fold them in definition although they are represented as tree of elements.

For example ``[GROUP]`` begin new group of components until next ``[GROUP]``. So if you need to put some elements vertically and horizontally you have to separate them by several ``[GROUP]`` elements.


```
[EDIT type=SELECT]
[OPTION text='First option']
[OPTION text='Second option']
```

## Parser

### Outer expression

Take this one as a test.

```
//[IGNORE][TEXT text=❤️]
[IMAGE
icon='logo'
//hint='[IMAGE]'
text='
That''s my job
'
]
/*
[GROUP orientation='VERTICAL']
[EDIT text='' hint='User']
//*/
[TEXT x="""💩"""]
///*
/* [EDIT text='' type='PASSWORD' hint='Password'] */
[CHECK text='Remember me']
[GROUP]
[TEXT text='Need account?']
[LINK text='Register here' target='http://target.link']
//
```

``PCRE (PHP) + ECMAScript (JavaScript)``

```
/\/\/[^\r\n]*(?:\r?\n|$)|\/\*(?:.|[\r\n])*?(?:\*\/|$)|(\[(?:\/\/[^\r\n]*(?:\r?\n|$)|\/\*(?:.|[\r\n])*?(?:\*\/|$)|'[^']*'|\"[^\"]*\"|[^\]])*\])/g
```

``C#``

```csharp
new Regex(@"
\/\/[^\r\n]*(?:\r?\n|$)
|
\/\*(?:.|[\r\n])*?(?:\*\/|$)
|
(
\[
(?:
  \/\/[^\r\n]*(?:\r?\n|$)
  |
  \/\*(?:.|[\r\n])*?(?:\*\/|$)
  |
  '[^']*'
  |
  \""[^\""]*\""
  |
  [^\]]
)*
\]
)
", RegexOptions.IgnoreWhitespace | RegexOptions.Multiline);
```

``Python``

```python
r"\/\/[^\r\n]*(?:\r?\n|$)|\/\*(?:.|[\r\n])*?(?:\*\/|$)|(\[(?:\/\/[^\r\n]*(?:\r?\n|$)|\/\*(?:.|[\r\n])*?(?:\*\/|$)|'[^']*'|\"[^\"]*\"|[^\]])*\])"g
```

``Golang``

```
`\/\/[^\r\n]*(?:\r?\n|$)|\/\*(?:.|[\r\n])*?(?:\*\/|$)|(\[(?:\/\/[^\r\n]*(?:\r?\n|$)|\/\*(?:.|[\r\n])*?(?:\*\/|$)|'[^']*'|\"[^\"]*\"|[^\]])*\])`g
```


[https://regex101.com/r/IK8B3r/1](https://regex101.com/r/IK8B3r/1)


## Bytecode

It is also possible to convert UI definition into bytecode. All texts are UTF-8 encoded.

Header starts with 4 bytes BB BB 00 01, where last one indicate version of bytecode.

Data contain only definitions and text values.

It will use snappy compression by default.

Each definition contains one character indicating type, like "T" for text, number of elements limited to 255, and a list of key - value pairs. We are however limited to 65535 elements of key and values.

All keys are converted to lowercase and there is a limitation of 65535 elements in one bytecode file.

'T' 02 00 01 00 02 00 03 00 04

00 01 : 00 04 'text'
00 02 : 00 07 'My text'
00 03 : 'key'
00 04 : 'X'

There should be two tables, one for keys and one for values, because there is no point in having more than 255 keys I guess. 

```
[E option='Option 1' option='Option 2' display=select]
```

## Example

### Sign in

This is example of sign in screen.

```
[IMAGE icon='logo']

[GROUP orientation='VERTICAL']
[EDIT text='' hint='User']
[EDIT text='' type='PASSWORD' hint='Password']
[CHECK text='Remember me']

[GROUP orientation='HORIZONTAL']
[BUTTON text='Sign in']
[BUTTON text='Forgotten password']

[GROUP]
[TEXT text='Need account?']
[LINK text='Register here' target='http://target.link']
```

The same using short version.

```
[I 'logo']

[G V]
[E hint='User']
[E type=PASSWORD hint='Password']
[C 'Remember me']

[G H]
[B 'Sign in']
[B 'Forgotten password']

[G]
[T 'Need account?']
[L 'Register here' target='http://target.link']
```

And here is its JSON representation.

```json
[
    {
        "_": "IMAGE",
        "icon": "logo"
    },
    {
        "_": "GROUP",
        "orientation": "VERTICAL"
    },
    {
        "_": "EDIT",
        "text": "",
        "hint": "User"
    },
    {
        "_": "EDIT",
        "text": "",
        "type": "PASSWORD",
        "hint": "Password"
    },
    {
        "_": "CHECK",
        "text": "Remember me"
    },
    {
        "_": "GROUP",
        "orientation": "HORIZONTAL"
    },
    {
        "_": "BUTTON",
        "text": "Sign in",
    },
    {
        "_": "BUTTON",
        "text": "Forgotten password"
    },
    {
        "_": "TEXT",
        "text": "Need account?"
    },
    {
        "_": "LINK",
        "text": "Register here",
        "target": "http://target.link"
    }
]
```

Simple and not so smart conversion from JSON to XML.

```xml
<?xml version="1.0" encoding="UTF-8"?>
<root>
   <element>
      <_>IMAGE</_>
      <icon>logo</icon>
   </element>
   <element>
      <_>GROUP</_>
      <orientation>vertical</orientation>
   </element>
   <element>
      <_>EDIT</_>
      <hint>User</hint>
      <text />
   </element>
   <element>
      <_>EDIT</_>
      <hint>Password</hint>
      <text />
      <type>PASSWORD</type>
   </element>
   <element>
      <_>CHECK</_>
      <text>Remember me</text>
   </element>
   <element>
      <_>GROUP</_>
      <orientation>vertical</orientation>
   </element>
   <element>
      <_>BUTTON</_>
      <text>Sign in</text>
   </element>
   <element>
      <_>BUTTON</_>
      <text>Forgotten password</text>
   </element>
   <element>
      <_>TEXT</_>
      <text>Need account?</text>
   </element>
   <element>
      <_>LINK</_>
      <target>http://target.link</target>
      <text>Register here</text>
   </element>
</root>
```

### Tabbed view

```
[USE 'tabs']
[BUTTON]
[FRAGMENT 'tabs']
[PAGE 'General']
[TEXT 'Name']          [EDIT]
[TEXT 'Description']   [EDIT]
[PAGE 'Additional']
[TEXT 'Modified by'] [TEXT]
```

### Multiple screens

```
[SCREEN]
[SCREEN]
```

### File browser

```
[SCREEN]
[DATA]
[GROUP v]
[BUTTON 'Ok']
[BUTTON 'Cancel']
```