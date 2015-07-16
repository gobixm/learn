using System;

namespace Infotecs.GangOfFour.Facade
{
    internal static class ReservationHelper
    {
        public static void ReserveRoom(string person, string roomNumber, DateTime enterTime, DateTime leaveTime)
        {
            dynamic repo = new Repo();

            dynamic room = repo.GetRoomByNumber(roomNumber);
            dynamic customer = repo.GetPerson(person);
            if (customer == null)
            {
                customer = repo.AddPerson(person);
            }
            room.Reserve(customer, enterTime, leaveTime);
        }
    }
}
