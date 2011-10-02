using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MudEngine.Core
{
    public abstract class BaseObject : IObject
    {
        #region Fields
        //Name of this object
        string _Name;
       
        //Object filename
        string _Filename;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets the Name for this object.  The objects Filename property is automatically set to match the Name.
        /// </summary>
        [Category("Object Information")]
        [Description("Provides a name for this object")]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                if (this.GetType().Name.StartsWith("Base"))
                    Filename = value + "." + this.GetType().Name.Substring("Base".Length);
                else
                    Filename = value + "." + this.GetType().Name;
            }
        }

        public int ID { get; protected set; }

        /// <summary>
        /// Gets or Sets the Filename used to save/load this object.  The Filename is automatically set/changed when the Name property changes
        /// </summary>
        [Category("Object Information")]
        [Description("Sets the Filename used to Save/Load this object.  The Filename is automatically set/changed when the Name property is changed.")]
        public string Filename
        {
            get
            {
                return _Filename;
            }
            set
            {
                if (this.GetType().Name.StartsWith("Base"))
                {
                    if (value.EndsWith("." + this.GetType().Name.Substring("Base".Length)))
                    {
                        _Filename = value;
                    }
                    else
                        _Filename = value + "." + this.GetType().Name.Substring("Base".Length);
                }
                else
                {
                    if (value.EndsWith("." + this.GetType().Name))
                    {
                        _Filename = value;
                    }
                    else
                        _Filename = value + "." + this.GetType().Name;
                }
            }
        }

        /// <summary>
        /// Gets or Sets a description of what this object is in the game world.
        /// </summary>
        [Category("Object Information")]
        [Description("Sets the description used to explain what this object is within the game world.")]
        public string Description { get; set; }
        #endregion

        public BaseObject()
        {
            this.Name = DefaultName();
        }

        #region Public Methods

        /// <summary>
        /// Returns the default name generated for this object by the engine.
        /// </summary>
        /// <returns></returns>
        protected virtual string DefaultName()
        {
            return "New " + this.GetType().Name;
        }

        public override string ToString()
        {
            return this.Name;
        }
        #endregion


        public virtual void Load(string objectName)
        {
            throw new NotImplementedException();
        }

        public virtual void Save()
        {
            throw new NotImplementedException();
        }
    }
}
