using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace BenDraw.PanelTools
{
    internal class Toolbar
    {
        IDictionary<int, bool> tools;
        public Toolbar()
        {
            tools = new Dictionary<int, bool>();
        }

        public void LoadTools()
        {
            for(int i = 1; i < 11; i++)
            {
                if(i==1)
                {
                    tools.Add(i, true);
                } else
                {
                    tools.Add(i, false);
                }
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

            return 1;
        }

        public void SetState(int index)
        {
            if(tools.ContainsKey(index))
            {
                foreach (var tool in tools)
                {
                    if (tool.Value)
                    {

                        tools.Add(tool.Key, false);
                    }
                }
                tools.Add(index, true);
            }
            
        }


    }
}
