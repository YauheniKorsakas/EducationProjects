using Education.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Behavioral.Observer
{
    public class ObserverCase : ICase
    {
        public async Task RunAsync() {
            var zheka = new Subscriber { Id = 1, Name = "zheka" };
            var artsiom = new Subscriber { Id = 2, Name = "artsiom" };
            var sergey = new Subscriber { Id = 3, Name = "sergey" };

            var newsStorage = new NewsStorage();
            newsStorage.Subscribe(artsiom);
            newsStorage.Subscribe(sergey);
            newsStorage.Subscribe(zheka);
            var newArticle = new NewsArticle { Id = 1, Name = "YAUHENI MADE A BIG SHIT", Content = "SHIT" };
            newsStorage.AddNewsArticle(newArticle);
            newsStorage.Unsubscribe(sergey);
            newArticle = new NewsArticle { Id = 2, Name = "YAUHENI MADE another BIG SHIT", Content = "WOOOOW" };
            newsStorage.AddNewsArticle(newArticle);
        }

        public interface IUser
        {
            int Id { get; set; }
            string Name { get; set; }
        }

        public interface ISubscriber<T> : IUser where T: NewsArticle
        {
            void Update(T data);
        }

        public interface IObservable<T> where T: NewsArticle
        {
            void Subscribe(ISubscriber<T> subscriber); // could return subscribtion object
            void Unsubscribe(ISubscriber<T> subscriber);
        }

        public interface INewsStorage<T> : IObservable<T> where T: NewsArticle
        {
            void AddNewsArticle(NewsArticle article);
        }

        public class Subscriber : ISubscriber<NewsArticle>
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public void Update(NewsArticle article) {
                Console.WriteLine($"User {Name} with id {Id} got article under name - {article.Name}");
            }
        }

        public class NewsArticle
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Content { get; set; }
        }

        public class NewsStorage : INewsStorage<NewsArticle>
        {
            private readonly HashSet<ISubscriber<NewsArticle>> subscriptions = new HashSet<ISubscriber<NewsArticle>>();
            private readonly HashSet<NewsArticle> news = new HashSet<NewsArticle>();

            public void Unsubscribe(ISubscriber<NewsArticle> subscriber) {
                subscriptions.Remove(subscriber);
                Console.WriteLine($"User {subscriber.Name} unsubscribed.");
            }

            public void Subscribe(ISubscriber<NewsArticle> subscriber) {
                subscriptions.Add(subscriber);
                Console.WriteLine($"User {subscriber.Name} subscribed.");
            }

            public void AddNewsArticle(NewsArticle article) {
                news.Add(article);
                Console.WriteLine($"New article '{article.Name}' was added to storage.");
                NotifySubscribers(article);
            }

            private void NotifySubscribers(NewsArticle article) {
                foreach (var item in subscriptions) {
                    item.Update(article);
                }
            }
        }
    }
}
