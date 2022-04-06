using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace BenDraw.PanelTools
{
    internal class Toolbar
    {
        IDictionary<int, bool> tools;
        int startIndex;
        Button highlighted;
        public Toolbar(Button defaultHighlighted)
        {
            tools = new Dictionary<int, bool>();
            startIndex = 1;
            highlighted = defaultHighlighted;
            SetHighlighted(defaultHighlighted);
        }

        public void LoadTools()
        {
            for(int i = 0; i < 11; i++)
            {
                if(i==0)
                {
                    tools.Add(startIndex, true);
                } else
                {
                    tools.Add(startIndex, false);
                }

                startIndex++;
            }
        }

        public int GetState()
        {
            foreach(var tool in tools)
            {
                if(tool.Value)
                {
                    return tool.Key;
                }
            }

            return -1;
        }

        public void SetState(int index, Button selected)
        {
            if (tools.ContainsKey(index))
            {
                for(int i = 1; i < tools.Count; i++)
                {
                    if (tools[i])
                    {
                        tools[i] = false;
                        tools[index] = true;
                        SetHighlighted(selected);
                    } 
                }
            }
        }

        public void SetHighlighted(Button selected)
        {
            Button lastHighlighted = highlighted;
            highlighted = selected;
            lastHighlighted.BackColor = Color.WhiteSmoke;
            highlighted.BackColor = Color.Gainsboro;
        }

    }
}
