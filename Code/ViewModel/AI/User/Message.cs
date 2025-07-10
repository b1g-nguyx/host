using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.AI.User
{
    public class Message
    {
        public string role { get; set; } = "";     // "user" hoặc "assistant"
        public string content { get; set; } = "";
    }
}
