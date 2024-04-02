using System.Runtime.InteropServices;
using Upr4;
class Program
{
    static void Main(string[] args)
    {
        int choice = -1;
        List<Contact> contacts = new List<Contact>();
        List<Message> messages = new List<Message>();
        ChatRoom chatRoom = new ChatRoom();

        while (true)
        {
            Console.WriteLine("Enter menu option: 1 to 4");
            Console.WriteLine("1) Create Contact");
            Console.WriteLine("2) Create Message");
            Console.WriteLine("3) Add message to Chat Room");
            Console.WriteLine("4) Print Chat Room Tuple");
            Console.WriteLine("5) Exit Program");
            Console.WriteLine("Enter choice: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    contacts.Add(CreateContact());
                    break;
                case 2:
                    messages.Add(CreateMessage(contacts));
                    break;
                case 3:
                    Message messageToAdd = ChooseMessageToAdd(messages);
                    chatRoom.AddMessage(messageToAdd);
                    break;
                case 4:
                    var chatRoomTuple = chatRoom.GetChatRoomTuple();
                    chatRoom.PrintChatRoomStats(chatRoomTuple);
                    break;
                case 5:
                    // exit the loop and program
                    break;
                default:
                    throw new NotImplementedException("This part is not implemented");
            }

            if (choice == 5) break;
        }
    }

    static Contact CreateContact()
    {
        Console.WriteLine("Enter Contact Name: ");
        string contactName = Console.ReadLine();

        Console.WriteLine("Enter Contact Email: ");
        string contactEmail = Console.ReadLine();

        return new Contact(contactName, contactEmail);
    }

    static Message CreateMessage(List<Contact> contacts)
    {
        DisplayAvailableContacts(contacts);
        Contact author = CreateContact();
        Console.WriteLine("Enter Message text: ");
        string messageText = Console.ReadLine();

        return new Message(author, messageText);
    }

    static Message ChooseMessageToAdd(List<Message> messages)
    {

        int messageId;
        do
        {
            for (int i = 0; i < messages.Count; i++)
            {
                Console.WriteLine($"id: {i}\n{messages[i].ContactText}");
            }

            Console.WriteLine("Choose message id: ");
            messageId = int.Parse(Console.ReadLine());
        }
        while (messageId < 0 || messageId > messages.Count);

        return messages[messageId];
    }

    static void DisplayAvailableContacts(List<Contact> contacts) =>
        contacts.ForEach(contact => Console.WriteLine(contact.ContactName));
}