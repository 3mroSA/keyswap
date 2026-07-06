# KeySwap
A simple tool in C# that instantly converts selected text between keyboard layout
This is my first program made in c#

## Usage
Have you ever been typing while looking at your keyboard and not noticing that you were typing in a different language?
I made this program to fix this, select the text you want to convert, press CTRL + Shift + K and your text has been converted to the layout chosen.

## Customizability
You are able to change and add more layouts. 
By Default the layouts are 
english -> arabic and vice versa.

If you plan on makinag a layout, heres the template.
Each line follows this format:
```
a=ش
b=لا
c=ؤ
```
The left side is the source character. Must be one character maximum
The right side is the text that replaces it ,it can be one or more characters.

## Limitations
This was originally made as an experiment so there are a couple rough edges,

somtimes, the program might output "cv", if that happens have clear your clipboard and it'll work the next time
Converting the text to arabic will leave an extra K at the end because the shortcut to activate, this is probably fixable but idk how, feel free to contribute if you could
multi character source mappings are unsupported, like لا=b because the source is two characters
# Adding a layout
Drag the .kmap file into the same folder as the exe and relaunch the exe, then right click the program in your taskbar and choose the new layout you added


# How it works
Basically you select the text, the program runs ctrl c to copy it,
then, it checks the clipboard to check each character and maps it to the other layout and save the result and then it does ctrl v



# Contributing
contributions are welcome, feel free to submit an issue (except the ones i mentioned earlier) or a pull request
You could also add a layout if you'd like 
