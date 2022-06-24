namespace RPG.NewSaving
{
    public interface NewISaveable
    {
        object CaptureState();
        void RestoreState(object state);
    }
}