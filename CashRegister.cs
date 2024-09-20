using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_HW_18_09_2024
{
    class CashRegister
    {
        private readonly Queue<Customer> _queue;
        private readonly int _registerId;

        public CashRegister(int registerId, Queue<Customer> queue)
        {
            _registerId = registerId;
            _queue = queue;
        }

        public void ProcessCustomers()
        {
            while (true)
            {
                Customer customer;
                lock (_queue)
                {
                    if (_queue.Count == 0)
                    {
                        break;
                    }
                    customer = _queue.Dequeue();
                }

                Console.WriteLine($"Касса {_registerId}: Начало обслуживания покупателя {customer.Id}");
                Random random = new Random();
                int processingTime = random.Next(1000, 3001);
                Thread.Sleep(processingTime);
                Console.WriteLine($"Касса {_registerId}: Завершение обслуживания покупателя {customer.Id} за {processingTime} мс");
            }
        }
    }
}
