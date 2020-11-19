using System;
using System.Collections.Generic;

namespace CommandIO
{
    class Program
    {
        static void Main(string[] args)
        {
            Forum forum = new Forum();
            Moderator moderator = new Moderator();

            AddComment comment1 = new AddComment("nie mam pomidorow", forum, "Ala");
            AddComment comment2 = new AddComment("nie mam ogorkow", forum, "Tom");

            moderator.StoreCommand(comment1);
            moderator.StoreCommand(comment2);

            moderator.ExecuteAll();
        }
    }

    public interface ICommand
    {
        void Execute();

        string GetComment();
    }

    public class Moderator
    {
        private List<ICommand> _commandList = new List<ICommand>();

        public void StoreCommand(ICommand command)
        {
            this._commandList.Add(command);
        }

        public void ExecuteAll()
        {
            foreach (ICommand command in this._commandList)
            {
                Console.WriteLine("Checking if we can execute command: " + command.GetComment());
                if (!command.GetComment().Contains("pomidor"))
                    command.Execute();
            }

            this._commandList.Clear();
        }
    }

    public class Forum
    {
        private List<Comment> _commentList = new List<Comment>();

        public void AddComment(string text, string author)
        {
            Comment comment = new Comment(text, author);
            this._commentList.Add(comment);
            Console.WriteLine("Comment was added: " + comment.GetComment() + ", by: " +
                comment.GetAuthor() + ", at: " + comment.GetDate());
        }
    }

    public class AddComment : ICommand
    {
        private string _comment;
        private Forum _forum;
        private string _author;

        public AddComment(string _comment, Forum _forum, string _author)
        {
            this._comment = _comment;
            this._forum = _forum;
            this._author = _author;
        }

        public void Execute()
        {
            this._forum.AddComment(this._comment, this._author);
        }

        public string GetComment()
        {
            return this._comment;
        }
    }

    public class Comment
    {
        private string _comment;
        private DateTime _date;
        private string _author;

        public Comment(string _comment, string _author)
        {
            this._author = _author;
            this._comment = _comment;
            this._date = DateTime.Now;
        }

        public string GetComment()
        {
            return this._comment;
        }

        public string GetAuthor()
        {
            return this._author;
        }

        public DateTime GetDate()
        {
            return this._date;
        }
    }
}
