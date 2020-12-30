using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkarDemoApp
{
        public class LinkarMainClass : LinkarClass
        {
            public enum StatusTypes { READED, NEW, MODIFY, DELETED, NONE };
            private StatusTypes _status = StatusTypes.NONE;

            public List<string> LstErrors;

            public StatusTypes Status
            {
                get
                {
                    return _status;
                }
                set
                {
                    _status = value;
                }
            }

            /// <summary>
            /// Original Record content for use in optimistic blocks		    
            /// </summary>
            private string _RecordOriginalContent;

            public string RecordOriginalContent
            {
                get
                {
                    return _RecordOriginalContent;
                }
                set
                {
                    _RecordOriginalContent = value;
                }
            }

            /// <summary>
            /// Original Record Itypes content for use in cancel operations
            /// </summary>
            private string _RecordOriginalContentItypes;

            public string RecordOriginalContentItypes
            {
                get
                {
                    return _RecordOriginalContentItypes;
                }
                set
                {
                    _RecordOriginalContentItypes = value;
                }
            }

        }

        public class LinkarClass : INotifyPropertyChanged
        {

            #region Constructor
            /// <summary>
            /// Constructs an instance of the base class.
            /// </summary>
            protected LinkarClass()
            {

            }
            #endregion

            #region INotifyPropertyChanged Members
            /// <summary>
            /// Defines the event raised when a property is changed
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string name)
            {
                System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new System.ComponentModel.PropertyChangedEventArgs(name));
                }
            }
            #endregion
        }
}
