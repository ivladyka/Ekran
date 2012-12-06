using System;
using Telerik.Web.UI;

/// <summary>
/// Summary description for EventArgs
/// </summary>
    public class EditableGridPrintEventArgs : EventArgs
    {
        GridEditableItem[] m_printedItems;

        public GridEditableItem[] PrintedItems
        {
            get { return this.m_printedItems; }
        }

        public EditableGridPrintEventArgs(GridEditableItem[] items)
        {
            this.m_printedItems = items;
        }
    }

    public class EditableGridDeleteEventArgs : EventArgs
    {
        GridEditableItem deletedItem;

        public GridEditableItem DeletedItem
        {
            get { return this.deletedItem; }
        }

        public EditableGridDeleteEventArgs(GridEditableItem item)
        {
            this.deletedItem = item;
        }
    }

    public class EditableGridMoveEventArgs : EventArgs
    {
        GridEditableItem[] movedItems;

        public GridEditableItem[] MovedItems
        {
            get { return this.movedItems; }
        }

        public EditableGridMoveEventArgs(GridEditableItem[] items)
        {
            this.movedItems = items;
        }
    }

    public class SelectedItemChangedEventArgs : EventArgs
    {
        private string newValue;
        private string newText;

        public string NewValue
        {
            get { return this.newValue; }
        }

        public string NewText
        {
            get { return this.newText; }
        }

        public SelectedItemChangedEventArgs(string newValue)
        {
            this.newValue = newValue;
        }

        public SelectedItemChangedEventArgs(string newText, string newValue)
        {
            this.newValue = newValue;
            this.newText = newText;
        }
    }
