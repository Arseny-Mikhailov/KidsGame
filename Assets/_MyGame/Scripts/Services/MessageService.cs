using UI;

namespace Services
{
    public class MessageService
    {
        private readonly MessageView _view;

        public MessageService(MessageView view)
        {
            _view = view;
        }

        public void Show(string text)
        {
            _view.ShowMessage(text);
        }
    }
}