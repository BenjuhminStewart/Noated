using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace BenDraw.PanelTools
{
    internal class Toolbar
    {
        IDictionary<int, bool> tools;
        int startIndex;
        public Toolbar()
        {
            tools = new Dictionary<int, bool>();
            startIndex = 1;
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

        public void SetState(int index)
        {
            if (tools.ContainsKey(index))
            {
                for(int i = 1; i < tools.Count; i++)
                {
                    if (tools[i])
                    {
                        tools[i] = false;
                        tools[index] = true;
                    }
                }
            }
        }


    }
}
