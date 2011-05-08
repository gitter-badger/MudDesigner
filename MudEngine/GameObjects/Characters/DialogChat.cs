using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.GameObjects.Characters
{
    public enum DialogSlot
    {
        Entry1,
        Entry2,
        Entry3,
        Entry4,
        Entry5,
    }

    public class DialogChat
    {
        public Dictionary<DialogSlot, DialogChat> DialogBranch { get; internal set; }

        public String Response { get; set; }
        public String Question { get; set; }

        public DialogChat()
        {
            DialogBranch = new Dictionary<DialogSlot, DialogChat>();
        }

        public DialogChat(String response) : this()
        {
            this.Response = response;
        }

        public Boolean AddDialog(DialogSlot slot, DialogChat dialog)
        {
            foreach (DialogSlot s in DialogBranch.Keys)
            {
                //If an entry is already in use, do not replace it or add a duplicate entry with a different dialog.
                //Reject it.
                if (s == slot)
                {
                    return false;
                }
            }

            DialogBranch.Add(slot, dialog);
            return true;

        }

        public DialogChat GetDialog(DialogSlot slot)
        {
            if (DialogBranch.Count == 0)
                return null;

            if (DialogBranch[slot] == null)
                return null;

            return DialogBranch[slot];
        }
    }
}
