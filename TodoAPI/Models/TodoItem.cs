namespace TodoAPI.Models {

    public class TodoItem {

        // ID will be the unique key inside the DB
        public long Id { get; set; }
        public long Name{ get; set; }
        // Flag to check whether task has been completed
        public long IsComplete { get; set; }

    }
}
