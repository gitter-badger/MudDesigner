using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MUDEngine.Objects;

namespace MUDEngine.Controls
{
    public partial class RoomControl : VisualContainer
    {
        public RoomControl(BaseObject EngineObject) :base(EngineObject)
        {
            InitializeComponent();
            //this.GameObject = EngineObject;
        }
    }
}
