using PBServer;

namespace PBServer.src.model.friends
{
    public class Friends
    {
        public int owner_id;
        public int friend_id;

        public int getFriendId()
        {
            return this.friend_id;
        }

        public int getOwnerId()
        {
            return this.owner_id;
        }
    }
}
