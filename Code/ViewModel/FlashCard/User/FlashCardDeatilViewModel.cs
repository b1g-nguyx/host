using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;
using ViewModel.Region.User;

namespace ViewModel.FlashCard.User
{
    public class FlashCardDeatilViewModel
    {

        public int LessonId { get; set; }
        public List<FlashCardViewModel> FlashCards { get; set; } = new List<FlashCardViewModel>();
        public LeftSlideViewModel LeftSlide { get; set; } = new LeftSlideViewModel();
        public string? Title { get; set; }
    }
}
