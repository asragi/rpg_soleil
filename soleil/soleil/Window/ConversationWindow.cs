namespace Soleil
{
    class ConversationWindow: UIImage
    {
        public ConversationWindow()
            :base(TextureID.MessageWindow,
                 new Vector(0, 200),
                 new Vector(0, 50),
                 DepthID.MessageBack)
        {

        }
    }
}