namespace Taskmangaer.models
{
  public class task
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime DueDate { get; set; }
            public Priority TaskPriority { get; set; }
            public string Category { get; set; }
            public Status TaskStatus { get; set; }
            public User AssignedUser { get; set; }

            public enum Priority
            {
                LOW, MEDIUM, HIGH
            }

            public enum Status
            {
                TODO, IN_PROGRESS, DONE
            }

            
        }

        
    }

