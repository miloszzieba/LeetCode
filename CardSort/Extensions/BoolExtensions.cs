using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Bool
{
    public static int ToInt(this bool value)
    {
        return value ? 1 : 0;
    }

    public static int CountTrue(params bool[] args)
    {
        return args.Count(x => x);
    }
}
