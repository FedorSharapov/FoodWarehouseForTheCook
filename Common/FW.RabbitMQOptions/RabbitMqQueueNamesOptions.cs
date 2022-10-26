namespace FW.RabbitMQOptions
{
    public class RabbitMqQueueNamesOptions
    {
        public const string KeyValue = "RabbitMQ:QueueNames";

        public QueueNames ChangesProducts { get; set; }
        public QueueNamesDishes Dishes { get; set; }
        public QueueNames Ingredients { get; set; }
        public QueueNames Recipes { get; set; }
        public QueueNamesWithGetByUserId Warehouses { get; set; }
    }
    public class QueueNames
    {
        public string Get { get; set; }
        public string GetPage { get; set; }
        public string GetAll { get; set; }
        public string Count { get; set; }
        public string Create { get; set; }
        public string Update { get; set; }
        public string Delete { get; set; }

        public virtual string[] AllNames
        { 
            get 
            {
                return new string[] { Get, GetPage, GetAll, Count, Create, Update, Delete }; 
            }
        }
    }
    public class QueueNamesWithGetByUserId : QueueNames
    {
        public string GetByUserId { get; set; }
        public override string[] AllNames
        {
            get
            {
                return new string[] { Get, GetByUserId, GetPage, GetAll, Count, Create, Update, Delete };
            }
        }
    }

    public class QueueNamesDishes : QueueNames
    {
        public string Cook { get; set; }
        public override string[] AllNames
        {
            get
            {
                return new string[] { Get, GetPage, GetAll, Count, Create, Update, Delete, Cook };
            }
        }
    }
}
