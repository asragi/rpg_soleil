namespace Soleil
{
    interface IMessageBox
    {
        void SetMessage(string text);
        bool GetAnimIsEnd();
        void FinishAnim();
    }
}