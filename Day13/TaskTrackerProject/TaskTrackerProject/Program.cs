using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;

namespace TaskTrackerProject
{
    public enum Statuses
    {
        Completed,
        Postponed,
        Active,
    }
    public enum Categories
    {
        Health,
        Pet,
        Home,
    }

    public record TaskGroupByStatus(Statuses Status, int Count, DateTime? ClosestDeadline);

    abstract class TaskBase
    {
        public event Action OnTaskExpired;
        private static int _idCounter = 1;

        public int Id { get; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        private Statuses _status;
        public Statuses Status
        {
            get => _status;
            set
            {
                if (value == Statuses.Completed)
                {
                    Deadline = null;
                    _status = value;
                }
                else
                {
                    _status = value;
                }
            }
        }

        protected TaskBase(string title, string? description, DateTime? deadline, Statuses status)
        {
            Id = _idCounter++;
            Title = title;
            Description = description;
            Deadline = deadline;
            Status = status;
        }

        protected TaskBase(int id, string title, string? description, DateTime? deadline, Statuses status)
        {
            Id = id;
            Title = title;
            Description = description;
            Deadline = deadline;
            Status = status;
            if (id >= _idCounter)
                _idCounter = id + 1;
        }

        public virtual void GetInfo()
        {
            Type type = this.GetType();
            var props = type.GetProperties();
            props = props.OrderBy(p => p.Name == "Id" ? 0 : 1).ThenBy(p => p.Name).ToArray();
            Console.WriteLine($"\n---- {type.Name} Info ----");
            foreach (PropertyInfo prop in props)
            {
                Console.WriteLine($"{prop.Name}: {prop.GetValue(this)}");
            }
            Console.WriteLine($"------------------\n");
        }

        public bool IsExpired() => Deadline is not null && Deadline <= DateTime.UtcNow;

        public void CheckAndNotifyIfExpired()
        {
            if (IsExpired())
                OnTaskExpired?.Invoke();
        }

        public string ToCsvLine()
        {
            string result = "";
            var props = this.GetType().GetProperties().OrderBy(p => p.Name == "Id" ? 0 : 1);
            foreach (var property in props)
            {
                if (result != "")
                    result += ",";
                if (property.Name == "Deadline")
                {
                    DateTime? deadline = (DateTime?)property.GetValue(this);
                    result += deadline?.ToString("O");
                }
                else
                    result += property.GetValue(this)?.ToString();
            }
            return result;
        }

        public override string ToString()
        {
            Type type = this.GetType();
            var props = type.GetProperties();
            props = props.OrderBy(p => p.Name == "Id" ? 0 : 1).ThenBy(p => p.Name).ToArray();
            string result = "";
            foreach (PropertyInfo prop in props)
            {
                if (result != "")
                    result += ", ";
                result += $"{prop.Name}: {prop.GetValue(this)}";
            }
            return result;
        }
    }

    class SchoolTask : TaskBase
    {
        public string SubjectName { get; set; }
        public bool IsTeamTask { get; set; }
        public SchoolTask(string title, string? description, DateTime? deadline, Statuses status, string subjName, bool isTeamTask)
            : base(title, description, deadline, status)
        {
            IsTeamTask = isTeamTask;
            SubjectName = subjName;
        }

        private SchoolTask(int id, string title, string? description, DateTime? deadline, Statuses status, string subjName, bool isTeamTask)
            : base(id, title, description, deadline, status)
        {
            IsTeamTask = isTeamTask;
            SubjectName = subjName;
        }
        public static SchoolTask Restore(int id, string title, string? description, DateTime? deadline, Statuses status, string subjName, bool isTeamTask)
        {
            return new SchoolTask(id, title, description, deadline, status, subjName, isTeamTask);
        }

    }

    class WorkTask : TaskBase
    {
        public string AssignedBy { get; set; }
        public WorkTask(string title, string? description, DateTime? deadline, Statuses status, string assignedBy)
            : base(title, description, deadline, status)
        {
            AssignedBy = assignedBy;
        }
        private WorkTask(int id, string title, string? description, DateTime? deadline, Statuses status, string assignedBy)
            : base(id, title, description, deadline, status)
        {
            AssignedBy = assignedBy;
        }
        public static WorkTask Restore(int id, string title, string? description, DateTime? deadline, Statuses status, string assignedBy)
        {
            return new WorkTask(id, title, description, deadline, status, assignedBy);
        }
    }

    class PersonalTask : TaskBase
    {
        public Categories Category { get; set; }
        public PersonalTask(string title, string? description, DateTime? deadline, Statuses status, Categories category)
        : base(title, description, deadline, status)
        {
            Category = category;
        }

        private PersonalTask(int id, string title, string? description, DateTime? deadline, Statuses status, Categories category)
            : base(id, title, description, deadline, status)
        {
            Category = category;
        }
        public static PersonalTask Restore(int id, string title, string? description, DateTime? deadline, Statuses status, Categories category)
        {
            return new PersonalTask(id, title, description, deadline, status, category);
        }
    }

    class TasksList<T> where T : TaskBase
    {
        private List<T> _tasksList = [];

        public ReadOnlyCollection<T> GetTasksList()
        {
            return _tasksList.AsReadOnly();
        }
        public T? GetTaskById(int id)
        {
            var task = _tasksList.FirstOrDefault(t => t.Id == id);
            if (task is not null)
                return task;

            Console.WriteLine($"[Getting Error] Task with specified id ({id}) does not exist.");
            return null;
        }
        public void Add(T task)
        {
            _tasksList.Add(task);
        }
        public void RemoveTaskById(int id)
        {
            var task = _tasksList.FirstOrDefault(t => t.Id == id);
            if (task is not null)
                _tasksList.Remove(task);
            else
                Console.WriteLine($"[REMOVING ERROR] Task with specified id ({id}) does not exist.");
        }
        public IEnumerable<T>? OrderByProperty(string prop) // works
        {
            Type type = typeof(T);
            PropertyInfo? property = type.GetProperty(prop);
            if (property is null)
            {
                Console.WriteLine($"The specified property was not found!");
                return null;
            }
            return _tasksList.OrderBy(t => property.GetValue(t));
        }
        public async Task<bool> UploadTasksToFile(string directory, string fileName)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            string path = Path.Combine(directory, fileName);

            if (File.Exists(path))
            {
                Console.WriteLine("[ERROR]: File in the specified path already exists!");
                return false;
            }

            try
            {
                using (var writer = new StreamWriter(path))
                {
                    string header = "";
                    var props = typeof(T).GetProperties().OrderBy(p => p.Name == "Id" ? 0 : 1);
                    foreach (var property in props)
                    {
                        if (header != "")
                            header += ",";
                        header += property.Name;
                    }
                    await writer.WriteLineAsync(header);
                    foreach (var task in _tasksList)
                    {
                        await writer.WriteLineAsync(task.ToCsvLine());
                    }
                }
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"The exception occured during uploading tasks: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected exception occured: {ex.Message}");
                return false;
            }
        }
        public static async Task<List<T>?> ReadTasksFromFile(string path, Func<Dictionary<string, int>, string[], T> f)
        {             
            List<T> tasks = new List<T>();

            if (!File.Exists(path))
            {
                Console.WriteLine($"[ERROR] The specified file does not exist!");
                return null;
            }
            try
            {
                using (var reader = new StreamReader(path))
                {
                    bool isFirstLine = true;
                    string[] header = [];
                    string? line;
                    Dictionary<string, int> propIndex;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        propIndex = [];
                        if (string.IsNullOrEmpty(line))
                            continue;

                        if (isFirstLine)
                        {
                            isFirstLine = false;
                            header = line.Split(',');
                            continue;
                        }

                        string[] data = line.Split(',');
                        for (int i = 0; i < header.Length; i++)
                        {
                            propIndex.Add(header[i], i);
                        }
                        tasks.Add(f.Invoke(propIndex, data));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected exception occured: {ex.Message}");
            }
            return tasks;
        }
        public IEnumerable<T> GetTasksBeforeDate(DateTime date)
        {
            return _tasksList.Where(t => t.Deadline < date && t.Deadline != null).ToList();
        }
        public IEnumerable<T> GetTasksWithoutDescription()
        {
            return _tasksList.Where(t => t.Description is null);
        }
        public List<TaskGroupByStatus> GroupTasksByStatus()
        {
            return _tasksList
                .GroupBy(t => t.Status)
                .Select(group => new TaskGroupByStatus(group.Key, group.Count(), group.Min(t => t.Deadline)))
                .ToList();
        }
    }
    internal class Program
    {
        static void WorkTaskNotification()
        {
            Console.WriteLine($"[CRITICAL] Work task is expired! Complete it immediately otherwise you may be fired!");
        }

        static void SchoolTaskNotification()
        {
            Console.WriteLine($"[IMPORTANT] School task is expired! Do not accumulate tasks as you may have problems!");
        }

        static void PersonalTaskNotification()
        {
            Console.WriteLine($"[MINOR] Personal task is expired! You should complete task on time!");
        }

        public static WorkTask WorkTaskBuilder(Dictionary<string, int> headerIndex, string[] propsValues)
        {
            int idIndex = headerIndex["Id"];
            int id = int.Parse(propsValues[idIndex]);

            int titleIndex = headerIndex["Title"];
            string title = propsValues[titleIndex];

            int descIndex = headerIndex["Description"];
            string? desc = propsValues[descIndex] == "" ? null : propsValues[descIndex];

            int deadlineIndex = headerIndex["Deadline"];
            DateTime? deadline;
            if (propsValues[deadlineIndex] == "")
                deadline = null;
            else
                deadline = DateTime.Parse(propsValues[deadlineIndex], CultureInfo.InvariantCulture);

            int statusIndex = headerIndex["Status"];
            Statuses status = Enum.Parse<Statuses>(propsValues[statusIndex]);

            int asssignedIndex = headerIndex["AssignedBy"];
            string assignedBy = propsValues[asssignedIndex];

            return WorkTask.Restore(id, title, desc, deadline, status, assignedBy);
        }

        public static SchoolTask SchoolTaskBuilder(Dictionary<string, int> headerIndex, string[] propsValues)
        {
            int idIndex = headerIndex["Id"];
            int id = int.Parse(propsValues[idIndex]);

            int titleIndex = headerIndex["Title"];
            string title = propsValues[titleIndex];

            int descIndex = headerIndex["Description"];
            string? desc = propsValues[descIndex] == "" ? null : propsValues[descIndex];

            int deadlineIndex = headerIndex["Deadline"];
            DateTime? deadline;
            if (propsValues[deadlineIndex] == "")
                deadline = null;
            else
                deadline = DateTime.Parse(propsValues[deadlineIndex], CultureInfo.InvariantCulture);

            int statusIndex = headerIndex["Status"];
            Statuses status = Enum.Parse<Statuses>(propsValues[statusIndex]);

            int subjNameIndex = headerIndex["SubjectName"];
            string subjectName = propsValues[subjNameIndex];

            int isTeamIndex = headerIndex["IsTeamTask"];
            bool isTeam = bool.Parse(propsValues[isTeamIndex]);

            return SchoolTask.Restore(id, title, desc, deadline, status, subjectName, isTeam);
        }

        public static PersonalTask PersonalTaskBuilder(Dictionary<string, int> headerIndex, string[] propsValues)
        {
            int idIndex = headerIndex["Id"];
            int id = int.Parse(propsValues[idIndex]);

            int titleIndex = headerIndex["Title"];
            string title = propsValues[titleIndex];

            int descIndex = headerIndex["Description"];
            string? desc = propsValues[descIndex] == "" ? null : propsValues[descIndex];

            int deadlineIndex = headerIndex["Deadline"];
            DateTime? deadline;
            if (propsValues[deadlineIndex] == "")
                deadline = null;
            else
                deadline = DateTime.Parse(propsValues[deadlineIndex], CultureInfo.InvariantCulture);

            int statusIndex = headerIndex["Status"];
            Statuses status = Enum.Parse<Statuses>(propsValues[statusIndex]);

            int categoryIndex = headerIndex["Category"];
            Categories category = Enum.Parse<Categories>(propsValues[categoryIndex]);

            return PersonalTask.Restore(id, title, desc, deadline, status, category);
        }

        static async Task Main()
        {
            SchoolTask schoolTask1 = new SchoolTask(
                "Complete C# homework",
                "Finish OOP exercises",
                DateTime.Now.AddDays(-3),
                Statuses.Active,
                "Programming",
                false);

            SchoolTask schoolTask2 = new SchoolTask(
                "Prepare presentation",
                "Prepare presentation for the lesson",
                DateTime.Now.AddDays(3),
                Statuses.Active,
                "Computer Science",
                true);

            SchoolTask schoolTask3 = new SchoolTask(
                "Write report",
                null,
                DateTime.Now.AddDays(5),
                Statuses.Postponed,
                "Database",
                false);

            WorkTask workTask1 = new WorkTask(
                "Fix API bug",
                null,
                DateTime.Now.AddDays(-1),
                Statuses.Active,
                "Alex");

            WorkTask workTask2 = new WorkTask(
                "Prepare documentation",
                "Update project documentation",
                DateTime.Now.AddDays(2),
                Statuses.Active,
                "John");

            WorkTask workTask3 = new WorkTask(
                "Deploy application",
                null,
                DateTime.Now.AddDays(7),
                Statuses.Completed,
                "Mike");

            PersonalTask personalTask1 = new PersonalTask(
                "Go to the gym",
                "One hour workout",
                DateTime.Now.AddDays(-1),
                Statuses.Active,
                Categories.Health);

            PersonalTask personalTask2 = new PersonalTask(
                "Take the dog for a walk",
                null,
                DateTime.Now.AddDays(3),
                Statuses.Completed,
                Categories.Pet);

            PersonalTask personalTask3 = new PersonalTask(
                "Clean the apartment",
                "Clean the kitchen and bedroom",
                DateTime.Now.AddDays(-3),
                Statuses.Postponed,
                Categories.Home);

            TasksList<SchoolTask> schoolTasksList = new TasksList<SchoolTask>();
            schoolTasksList.Add(schoolTask1);
            schoolTasksList.Add(schoolTask3);
            schoolTasksList.Add(schoolTask2);
            TasksList<PersonalTask> personalTasksList = new TasksList<PersonalTask>();
            personalTasksList.Add(personalTask1);
            personalTasksList.Add(personalTask2);
            personalTasksList.Add(personalTask3);
            TasksList<WorkTask> workTasksList = new TasksList<WorkTask>();
            workTasksList.Add(workTask1);
            workTasksList.Add(workTask2);
            workTasksList.Add(workTask3);

            //if (await schoolTasksList.UploadTasksToFile(@"E:\", "schoolTasks.csv"))
            //{
            //    Console.WriteLine($"File was created successfully!");
            //}
            //else
            //{
            //    Console.WriteLine($"An error occured!");
            //}
            //if (await personalTasksList.UploadTasksToFile(@"E:\", "personalTasks.csv"))
            //{
            //    Console.WriteLine($"File was created successfully!");
            //}
            //else
            //{
            //    Console.WriteLine($"An error occured!");
            //}
            //if (await workTasksList.UploadTasksToFile(@"E:\", "workTasks.csv"))
            //{
            //    Console.WriteLine($"File was created successfully!");
            //}
            //else
            //{
            //    Console.WriteLine($"An error occured!");
            //}

            personalTask2.OnTaskExpired += PersonalTaskNotification;
            personalTask3.OnTaskExpired += PersonalTaskNotification;
            personalTask2.CheckAndNotifyIfExpired(); // nothing happens
            personalTask3.CheckAndNotifyIfExpired(); // calls PersonalTaskNotification
            Console.WriteLine();
            var schoolList = schoolTasksList.GetTasksList();
            foreach (var item in schoolList)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
            var scTask = schoolTasksList.GetTaskById(1);
            var scTask2 = schoolTasksList.GetTaskById(4); // error message
            scTask?.GetInfo();

            schoolTasksList.RemoveTaskById(1);
            var schoolList2 = schoolTasksList.GetTasksList();
            foreach (var item in schoolList2)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
            var error_list = schoolTasksList.OrderByProperty("Teacher");
            var sl3 = schoolTasksList.OrderByProperty("Id"); // error message
            foreach (var item in sl3)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
            //var read_list = await TasksList<WorkTask>.ReadTasksFromFile(@"E:\workTasks.csv", WorkTaskBuilder);
            //foreach (var item in read_list)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            Console.WriteLine();
            var tasksBeforeDate = personalTasksList.GetTasksBeforeDate(DateTime.Now);
            foreach (var item in tasksBeforeDate)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
            var tasksWithoutDescription = workTasksList.GetTasksWithoutDescription();
            foreach (var item in tasksWithoutDescription)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
            var groups = workTasksList.GroupTasksByStatus();
            foreach (var item in groups)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}