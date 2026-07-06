using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using TextCopy;
using Gma.System.MouseKeyHook;

class App : ApplicationContext
{
    Dictionary<char, string> map = new();
    Dictionary<string, Dictionary<char, string>> packs = new();
    string active = "";

    IKeyboardMouseEvents keys;
    NotifyIcon tray;

    public App()
    {
        load();
        MessageBox.Show(
        $@"Loaded {map.Count} mappings

Active pack: {active}

Hotkey: Ctrl + Shift +  K

Press OK to continue.

More details:
github.com/3mroSA",
        "KeySwap",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information
        ); trayIcon();
        hotkey();
    }

    void load()
    {
        foreach (var file in Directory.GetFiles(AppContext.BaseDirectory, "*.kmap"))
        {
            var m = new Dictionary<char, string>();

            foreach (var line in File.ReadLines(file))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.StartsWith("#")) continue;
                if (!line.Contains("=")) continue;

                var parts = line.Split('=', 2);
                if (parts[0].Length != 1) continue;

                m[parts[0][0]] = parts[1];
            }

            var name = Path.GetFileNameWithoutExtension(file);

            packs[name] = m;

            if (active == "")
            {
                active = name;
                map = m;
            }
        }
    }

    void trayIcon()
    {
        tray = new NotifyIcon();
        tray.Icon = SystemIcons.Application;
        tray.Visible = true;
        tray.Text = "KeySwap";

        var menu = new ContextMenuStrip();

        foreach (var p in packs)
        {
            menu.Items.Add(p.Key, null, (s, e) =>
            {
                active = p.Key;
                map = p.Value;
            });
        }

        menu.Items.Add("-");
        menu.Items.Add("exit", null, (s, e) => ExitThread());

        tray.ContextMenuStrip = menu;
    }

    void hotkey()
    {
        keys = Hook.GlobalEvents();

        keys.KeyDown += (s, e) =>
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.K)
                run();
        };
    }

    void run()
    {
        SendKeys.SendWait("^c");
        System.Threading.Thread.Sleep(50);

        var text = ClipboardService.GetText();
        if (string.IsNullOrEmpty(text)) return;

        var outText = "";

        foreach (var c in text)
        {
            if (map.TryGetValue(c, out var v))
                outText += v;
            else
                outText += c;
        }

        ClipboardService.SetText(outText);
        SendKeys.SendWait("^v");
    }
}