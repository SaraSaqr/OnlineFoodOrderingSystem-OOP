using System.Numerics;

namespace PROJECT_6_OF_MASTERING_OOP
{
   abstract class FoodItem
    {
        public int ID;
        public string Name;
        private double Price;
      
        public void Setprice(double price)
        {
            Price = price;
        }
        public double Getprice()
        {
            return  Price;
        }

        public virtual double CalculatePrice()
        {
            return 0;
        }

    }

    class Meal:FoodItem
    {
        public override double CalculatePrice()
        {
            return Getprice()+20;
        }
    }


    class Dessert:FoodItem
    {
        public override double CalculatePrice()
        {
            return Getprice()+(Getprice()*.1);
        }
    }

    class Drink : FoodItem
    {
        public override double CalculatePrice()
        {
            return Getprice();
        }
    }


    interface Ipayment
    {
        void Pay(double total);

    }
    class Cash:Ipayment
    {
        public void Pay(double total)
        {
            Console.WriteLine(total+"PAID BY CASH");
        }
    }
    class Credit : Ipayment
    {
        public void Pay(double total)
        {
            Console.WriteLine(total + "PAID BY CREDIT CARD");
        }
    }





    class Customer
    {
        Ipayment ipayment;

        public int id;
        public string name;
        private double AccountBalance;
        public Customer(int i, string n, double a)
        {
            id = i;
            name = n;
            AccountBalance = a;
        }
        public double Getbalance()

        {
            return AccountBalance;
        }

        public void MAKEORDER(Order order)
        {
            order.Setcustomer(this);
        }

        public void CHECKOUT(Order order)
        {
            double total = order.TOTALPRICE();

            if (order.index == 0)
            {
                throw new Exception("ORDER IS EMPTY");
            }
            if (ipayment == null)
            {
                throw new Exception("you must choose ur payment method");
            }


            if (AccountBalance>=total)
            {
                ipayment.Pay(total);
                DeductBalance(total);
                Console.WriteLine("Completed");
                order.status = STATUS.COMPLATED;
            }

           


            
            else
            {
                throw new Exception("ACCOUNT BALANCE NOT ENOUGH");
            }

        }

        public void DeductBalance(double total)
        {
            AccountBalance -= total;
        }



        public void SetPay(Ipayment payment)
        {
            ipayment = payment;
        }








    }







    enum STATUS
    {
        NOTCOMPLATED,
        COMPLATED
    }


    class Order
    {
        public STATUS status;


        public double total=0;
        Customer customer;
        public void Setcustomer(Customer c)
        {
            customer = c;
        }
        public Customer GetCustomer()
        {
       return customer;
        }


        public int id;
        FoodItem[] items;
        public int index;
        public Order()
        {
            items = new FoodItem[100];
            index = 0;
        }
        public void ADDORDER(FoodItem foodItem )
        {

            if(foodItem==null)
            {
                throw new Exception("item is empty");
            }

          for(int i=0;i<index;i++)
            {
                if (items[i].ID == foodItem.ID)
                {
                    throw new Exception("not allowed");
                }
            }

            items[index++] = foodItem;
        }
        public void Display()
        {
            for(int i=0;i<index;i++)
            {
                Console.WriteLine("ITEM NAME : "+items[i].Name + "\n" +"WITH PRICE :"+ items[i].Getprice() + "\n" +"ID : "+ items[i].ID);
            }
        }

        public void DELETE(int id)
        {
            for (int i = 0; i < index; i++)
            {
                if (items[i].ID == id)
                {

                    for (int j = i; j < index - 1; j++)
                    {
                        items[j] = items[j + 1];
                    }
                    index--;
                    return;

                }

            }
        }

        public double TOTALPRICE()
        {
            for(int i=0;i<index;i++)
            {
                total += items[i].CalculatePrice();
            }
            return total;
        }
       
    }



    class Restaurant
    {
        FoodItem[] foodItems;
        public int index;

        public Restaurant()
        {
            foodItems = new FoodItem[100];
            index = 0;
        }


        public void ADDITEM(FoodItem food)
        {
          
            foodItems[index++] = food;
        }

        public void Display()
        {
            for (int i = 0; i < index; i++)
            {
                Console.WriteLine("ITEM NAME : " + foodItems[i].Name + "\n" + "WITH PRICE :" + foodItems[i].Getprice() + "\n" + "ID : " + foodItems[i].ID);
            }
        }

        public void DELETE(int id)
        {
            for (int i = 0; i < index; i++)
            {
                if (foodItems[i].ID == id)
                {

                    for (int j = i; j < index - 1; j++)
                    {
                        foodItems[j] = foodItems[j + 1];
                    }
                    index--;
                    return;

                }

            }
        }

        public bool Search(int id)
        {
            for (int i = 0; i < index; i++)
            {
                if (foodItems[i].ID == id)
                {

                    Console.WriteLine(" found ");
                    Console.WriteLine("ITEM NAME : " + foodItems[i].Name + "\n" + "WITH PRICE :" + foodItems[i].Getprice() + "\n" + "ID : " + foodItems[i].ID);
                    return true;
                   

                    
                }

            }
            Console.WriteLine("NOT FOUND");
            return false;
           


        }
    }






    internal class Program
    {
        static void Main(string[] args)
        {
            Restaurant restaurant = new Restaurant();
            FoodItem foodItem = new Meal();
            foodItem.Name = "SHRIMP";
            foodItem.ID = 1;
            foodItem.Setprice(1000);

            FoodItem foodItem2 = new Dessert();
            foodItem2.Name = "KONAFA";
            foodItem2.ID = 2;
            foodItem2.Setprice(200);


            FoodItem foodItem3 = new Drink();
            foodItem3.Name = "MANGO JUICE";
            foodItem3.ID = 3;
            foodItem3.Setprice(100);


            restaurant.ADDITEM(foodItem);
            restaurant.ADDITEM(foodItem2);
            restaurant.ADDITEM(foodItem3);

            Order order = new Order();
            order.ADDORDER(foodItem);
            order.ADDORDER(foodItem2);
         

            Customer customer = new Customer(0167,"SARA",20000);
            customer.MAKEORDER(order);
            Ipayment ipayment = new Cash();
            customer.SetPay(ipayment);
            customer.CHECKOUT(order);



        }
    }
}
