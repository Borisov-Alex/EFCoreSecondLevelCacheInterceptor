﻿using System.Linq;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using System;
using Issue9SQLiteInt32.Entities;

namespace Issue9SQLiteInt32
{
    class Program
    {
        static void Main(string[] args)
        {
            initDb();

            EFServiceProvider.RunInContext(context =>
            {
                var cachedPeople = context.People.Cacheable().ToList();
                cachedPeople = context.People.Cacheable().ToList();
                foreach (var person in cachedPeople)
                {
                    Console.WriteLine($"{person.Id}, {person.Name}");
                }

                cachedPeople = context.People.Cacheable(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(51)).ToList();
                cachedPeople = context.People.Cacheable(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(51)).ToList();
                foreach (var person in cachedPeople)
                {
                    Console.WriteLine($"{person.Id}, {person.Name}");
                }

                var cachedPerson = context.People.Cacheable().FirstOrDefault(x => x.UlongValue == 1);
                cachedPerson = context.People.Cacheable().FirstOrDefault(x => x.UlongValue == 1);
                Console.WriteLine($"{cachedPerson.Id}, {cachedPerson.Name}");
            });
        }

        private static void initDb()
        {
            EFServiceProvider.RunInContext(context =>
            {
                context.Database.Migrate();

                if (!context.People.Any())
                {
                    context.People.Add(new Person
                    {
                        Name = "Bill",
                        AddDate = DateTime.UtcNow,
                        UpdateDate = null,
                        Points = 1000,
                        IsActive = true,
                        ByteValue = 1,
                        CharValue = 'C',
                        DateTimeOffsetValue = DateTimeOffset.UtcNow,
                        DecimalValue = 1.1M,
                        DoubleValue = 1.3,
                        FloatValue = 1.2f,
                        GuidValue = Guid.NewGuid(),
                        TimeSpanValue = TimeSpan.FromDays(1),
                        ShortValue = 2,
                        ByteArrayValue = new byte[] { 1, 2 },
                        UintValue = 1,
                        UlongValue = 1,
                        UshortValue = 1,
                        NumericDecimalValue = 1.1M
                    });

                    context.People.Add(new Person
                    {
                        Name = "Vahid",
                        AddDate = DateTime.UtcNow,
                        UpdateDate = null,
                        Points = 1000,
                        IsActive = true,
                        ByteValue = 1,
                        CharValue = 'C',
                        DateTimeOffsetValue = DateTimeOffset.UtcNow,
                        DecimalValue = 1,
                        DoubleValue = 2,
                        FloatValue = 3,
                        GuidValue = Guid.NewGuid(),
                        TimeSpanValue = TimeSpan.FromDays(1),
                        ShortValue = 2,
                        ByteArrayValue = new byte[] { 1, 2 },
                        UintValue = 1,
                        UlongValue = 1,
                        UshortValue = 1,
                        NumericDecimalValue = 2
                    });

                    context.SaveChanges();
                }
            });
        }
    }
}