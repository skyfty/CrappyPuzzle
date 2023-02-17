using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Data
{
    [Serializable]
    public class Position
    {
        public int x;
        public int y;
        public int z;
    }
    [Serializable]
    public class Face
    {
        public string name;
        public string color;
    }
    [Serializable]
    public class Pack
    {
        public string pokes;
        public int cnt;
    }

    [Serializable]
    public class Components
    {
        public Position position;
        public Face face;
        public string direction;

        public string rand;
        public bool IsRand()            //随机
        {
            return this.rand != null && this.rand.Length > 0;
        }

        public string chromy;
        public bool IsChromy()          //单色
        {
            return this.chromy != null && this.chromy.Length > 0;
        }

        public bool wan = false;
        public bool IsWan()             //万能
        {
            return this.wan;
        }

        public string mulch;            //关联压牌id
        public bool IsMulch()
        {
            return this.mulch != null && this.mulch.Length > 0;
        }

        public string[] GetMulchIds()
        {
            if (mulch == null)
            {
                return null;
            }
            return this.mulch.Split(",");
        }

        public Pack pack;
        public bool IsPack()
        {
            return this.pack != null && this.pack.pokes != null && this.pack.pokes.Length > 0;
        }

        public string[] GetPackIds()
        {
            if (this.pack == null || this.pack.pokes == null)
            {
                return null;
            }
            return this.pack.pokes.Split(",");
        }


        public string variety;
        public bool IsVariety()             //万能
        {
            return this.variety != null;
        }

        public string display;

    }

    [Serializable]
    public class Card
    {
        public string id;
        public Components components;

        public bool IsHide()
        {
            return components.direction == "back";
        }

        public bool IsShow()
        {
            return components.display != "hide";
        }
    }

 
    [Serializable]
    public class Composition
    {
        public List<Card> cards;
        public List<Card> underpans;
    }

    [Serializable]
    public class StageParams
    {

    }

    [Serializable]
    public class UnderpanParams
    {

    }

    [Serializable]
    public class Level
    {
        public Composition composition;
    }

    [Serializable]
    public class Occasion
    {
        public string name;
        public string id;
        public Level level;

    }
}
