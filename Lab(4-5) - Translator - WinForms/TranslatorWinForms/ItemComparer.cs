using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace ListViewSortAnyColumn
{
    public class ItemComparer : IComparer
    {
        //column used for comparison
        public int Column { get; set; }
        //Order of sorting
        public SortOrder Order { get; set; }
        public ItemComparer(int colIndex)
        {
            Column = colIndex;
            Order = SortOrder.None;
        }
        public int Compare(object a, object b)
        {
            int result;
            ListViewItem itemA = a as ListViewItem;
            ListViewItem itemB = b as ListViewItem;
            if (itemA == null && itemB == null)
                result = 0;
            else if (itemA == null)
                result = -1;
            else if (itemB == null)
                result = 1;
            if (itemA == itemB)
                result = 0;
            //alphabetic comparison
            result = String.Compare(itemA.SubItems[Column].Text, itemB.SubItems[Column].Text);
            // if sort order is descending.
            if (Order == SortOrder.Descending)
                // Invert the value returned by Compare.
                result *= -1;
            return result;
        }
    }
}
