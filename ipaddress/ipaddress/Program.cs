using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace ipaddress
{
    class Program
    {
        // type safety with Dictionary & O(1) time complexity.
       static Dictionary<string, uint> dt = new Dictionary<string, uint>();
        /// <summary>
        /// This function accepts a string containing an IP address like “145.87.2.109”. This function will be called by the web service every time it handles a request. The calling code is outside the scope of this project. Since it is being called very often, this function needs to have a fast runtime.
        /// </summary>
        /// <param name="ip_address"></param>
        static void Request_Ipadress( string ip_address)
        {
            //We need to check If the Ip address is valid.
            if (ValidIPAddress(ip_address))
            {
                //If IP address is valid 
                if (!dt.ContainsKey(ip_address))
                {
                    dt.Add(ip_address, 1);
                }
                else
                {
                    //Increament the count by 1 if already in the dictionary.
                    dt[ip_address] += 1;
                }

            }

            //was not sure what was the requirement if the ip_address was invalid
           
        }

        static bool ValidIPAddress(string IP)
        {
            //Validate IP Address , neither IPV4, or V6

            if (IPAddress.TryParse(IP, out var address) == false)
                return false;
            //check for IPV6

            if (address.AddressFamily == AddressFamily.InterNetworkV6)

            {

                if (IP.IndexOf("::") > -1)

                    return true;

                return false;

            }

            //check for IPV4

            else

            {

                //Ipv4 address shouldn't start with 0 eg..it is invalid 0XX.0XX.0XX.0XX
                if (Regex.IsMatch(IP, @"(^0\d|\.0\d)"))

                    return false;

                else if (IP.Count(c => c == '.') != 3)

                    return false;

                else

                    return true;

            }

        }
        static IEnumerable<string> Top()
        {
            List<string> top_ip = null;
            if (dt.Count < 0)
                return null;

            if (dt.Count < 100)
                return top_ip = dt.OrderByDescending(x => x.Value)
                                .Select(x => x.Key)
                                .Take(top_ip.Count)
                                .ToList();
            else
            {
                return top_ip = dt.OrderByDescending(x => x.Value)
                                .Select(x => x.Key)
                                .Take(100)
                                .ToList();
            }
        }

        static void Clear()
        {
            dt?.Clear();
        }
        static void Main(string[] args)
        {
            for (int i = 0; i < 200; i++)
            {
                Request_Ipadress("192.168.1."+i.ToString());
            }
            Request_Ipadress("192.168.1.5");
            Request_Ipadress("192.168.1.5");
            Request_Ipadress("192.168.1.5");
            Request_Ipadress("192.168.1.5");
            Request_Ipadress("192.168.1.20");
            Request_Ipadress("192.168.1.22");
            Request_Ipadress("192.168.1.34");
            Request_Ipadress("192.168.1.35");
            Request_Ipadress("192.168.1");


            // Print the List
            var strArray = Top();
            foreach (var item in strArray)
            {
                Console.WriteLine(item);
            }
            //strArray.ForEach(Console.WriteLine);
            Clear();
            Console.WriteLine("Hello World!");
        }
    }
}
