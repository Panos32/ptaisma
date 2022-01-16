using System.Data;

internal static class Program
{
    private static void Main(string[] args)
    {

        String cityname;
        int offer;
        String continent;
        Console.WriteLine("Welcome. Please give us the number of the cities that you are going to add.");
        //Range of List by user

        int range = int.Parse(Console.ReadLine());

        //Object of the create City list

        var cityinfo = new List<City>(range);

        //Given information buy the user and then adding them to the List

        for (int i = 1; i <= range; i++)
        {
            Console.WriteLine("Give the name of the City (starting with a capital letter).");
            cityname = Console.ReadLine();
            Console.WriteLine("Give the exact ammount of the offer this City is giving us (without the dots).");
            offer = int.Parse(Console.ReadLine());
            Console.WriteLine("Give the Continent this City exists in (starting with a capital letter).");
            Console.WriteLine("IMPORTANT: If the City is in America, please give us the exact name following the examples given below.");
            Console.WriteLine("South America, North America.");
            continent = Console.ReadLine();
            cityinfo.Add(new City(cityname, offer, continent));
        }







        
      
            int x = 0;
            String y = String.Empty;

        //Foreach 'Method' to find the biggest given offer
        String cont = String.Empty;
        foreach (var item in cityinfo)
        {

            if (x < item.Offer)
            {
                x = item.Offer;
                y = item.CityName;
                cont = item.Continent;
            }
        }
        //Console Writes to check if this method works correctly

        Console.WriteLine("The max offer is " + x + " for " + y);
        Console.WriteLine("Since " + y + " has the best offer, the tour starts and finishes there.");

        //Creation of another List and adding the distance from the best offer city(starting
        //city) and every city that is going to follow the 'path'.

        String tempCityName = y;
        var continfo = new List<City>(range - 1);
        int distance = 0;
        foreach (var item in cityinfo)
        {
            if (tempCityName != item.CityName && cont == item.Continent)
            {

                Console.WriteLine("Please give us the distance between the cities in Kilometers (not using the km in the end): " + item.CityName + " -> " + tempCityName);
                distance = int.Parse(Console.ReadLine());
                continfo.Add(new City(item.CityName, item.Offer, item.Continent, distance));

            }
        }



        //Loop while all continent be complete
        while (true)
        {

            //Finding the complete offer by substracting offers and distances

            int completeoffer;
        foreach (var item in continfo.ToList())
        {
            completeoffer = item.Offer - item.Distance;
            continfo.Remove(item);
            continfo.Add(new City(item.CityName, completeoffer, item.Continent, item.Distance));

        }

        //Descending the Offers we took
        var DescendingList = continfo
                      .OrderByDescending(x => x.Offer)
                      .ToList();
        Console.WriteLine("The ending path of the circuit is: ");
        Console.Write(y + " -> ");

        //Foreach method to ready the descending list and see that it works correctly

        foreach (var item in DescendingList)
        {

            Console.Write(item.CityName + "");
            Console.Write(" -> ");

        }
        Console.Write(y);
        var useCont = new List<String>(5);


        DataTable ContDist = new DataTable();
        ContDist.Columns.Add("From", typeof(String));
        ContDist.Columns.Add("To", typeof(String));
        ContDist.Columns.Add("Kilometers", typeof(int));
        ContDist.Rows.Add("Europe", "Asia ", 6706);
        ContDist.Rows.Add("Europe ", " Africa ", 7262);
        ContDist.Rows.Add("Europe ", " Australia ", 14085);
        ContDist.Rows.Add("Europe ", " South America ", 9589);
        ContDist.Rows.Add("Europe ", " North America ", 6166);
        ContDist.Rows.Add("Asia ", " Europe  ", 6706);
        ContDist.Rows.Add("Asia ", " Africa ", 8418);
        ContDist.Rows.Add("Asia ", " Australia ", 7463);
        ContDist.Rows.Add("Asia ", " South America ", 11180);
        ContDist.Rows.Add("Asia ", " North America  ", 7824);
        ContDist.Rows.Add("Africa ", " Europe ", 7262);
        ContDist.Rows.Add("Africa ", " Asia ", 8418);
        ContDist.Rows.Add("Africa ", " Australia ", 10819);
        ContDist.Rows.Add("Africa ", " South America ", 9853);
        ContDist.Rows.Add("Africa ", " North America ", 13802);
        ContDist.Rows.Add("Australia ", " Europe ", 14085);
        ContDist.Rows.Add("Australia ", " Asia ", 7463);
        ContDist.Rows.Add("Australia ", " Africa ", 10819);
        ContDist.Rows.Add("Australia ", " South America ", 16087);
        ContDist.Rows.Add("Australia ", " North America ", 14241);
        ContDist.Rows.Add("South America ", " Europe ", 9589);
        ContDist.Rows.Add("South America ", " Asia ", 11180);
        ContDist.Rows.Add("South America ", " Africa ", 9853);
        ContDist.Rows.Add("South America ", " Australia ", 16087);
        ContDist.Rows.Add("South America ", " North America ", 8418);
        ContDist.Rows.Add("North America ", " Europe ", 6166);
        ContDist.Rows.Add("North America ", " Asia ", 7824);
        ContDist.Rows.Add("North America ", " Africa ", 13802);
        ContDist.Rows.Add("North America ", " Australia ", 14241);
        ContDist.Rows.Add("North America ", " South America ", 8418);

        String nextCont = String.Empty;
        int minDis = 0;
        foreach (DataColumn column in ContDist.Columns)
        {
            foreach (DataRow row in ContDist.Rows)
            {
                if (cont == row["From"].ToString())
                {


                    minDis = Convert.ToInt32(ContDist.Compute("min([Kilometers])", String.Empty));
                    useCont.Add(cont);

                    /* row.Delete();*/

                    /* if (minDis == (int)row["Kilometers"])
                     {
                         for (int i = ContDist.Rows.Count - 1; i >= 0; i--)
                     {
                         DataRow dataRow = ContDist.Rows[i];
                             row.Delete();     
                         ContDist.AcceptChanges();
                     }
                     }*/
                }


            }

        }
        foreach (DataColumn column in ContDist.Columns)
        {
            foreach (DataRow row in ContDist.Rows)
            {
                if (minDis == (int)row["Kilometers"])
                {
                    nextCont = row["To"].ToString();

                    break;
                }

            }
        }

        foreach (DataColumn column in ContDist.Columns)
        {
            foreach (DataRow row in ContDist.Rows)
            {
                Console.WriteLine(row[column]);
            }
        }

        Console.WriteLine("The closer cont is " + minDis + " " + nextCont);
        }
    }
}

//Class City 
public class City
{
    //Constractor one without the distance value
    public City(String cityName, int offer, String continent)
    {
        Offer = offer;
        CityName = cityName;
        Continent = continent;
    }

    //Constractor two with the distance value
    public City(String cityName, int offer, String continent, int distance)
    {
        Offer = offer;
        CityName = cityName;
        Continent = continent;
        Distance = distance;
    }



    //Getter for each variables

    public int Offer { get; private set; }

    public String Continent { get; private set; }

    public String CityName { get; private set; }
    public int Distance { get; private set; }
}



