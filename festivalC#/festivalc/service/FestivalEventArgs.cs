using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalc.service
{
    public enum UserEvent
    {
        BiletAdded
    };

    public class FestivalEventArgs : EventArgs
    {
        private readonly UserEvent userEvent;
        private readonly Object data;

        public FestivalEventArgs(UserEvent userEvent, object data)
        {
            this.userEvent = userEvent;
            this.data = data;
        }

        public UserEvent UserEventType
        {
            get { return userEvent; }
        }

        public object Data
        {
            get { return data; }
        }
    }
}
