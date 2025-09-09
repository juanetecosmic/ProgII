using Problema1_8.Domain;
using Problema1_8.Services;
using System;
//inserting clients
PetType dogType = new PetType { Id = 1, Description = "Perro" };
PetType catType = new PetType { Id = 2, Description = "Gato" };
PetType birdType = new PetType { Id = 3, Description = "Ave" };
PetType fishType = new PetType { Id = 4, Description = "Pez" };
ClientService clientService = new ClientService();
PetService petService = new PetService();
Pet pet1 = new Pet { Id = 0, Name = "Firulais", Type = dogType, Age = 2, Active = true };
Pet pet2 = new Pet { Id = 0, Name = "Michi", Type = catType, Age = 3, Active = true };
Pet pet3 = new Pet { Id = 0, Name = "Nemo", Type = fishType, Age = 1, Active = true };
Pet pet4 = new Pet { Id = 0, Name = "Tweety", Type = birdType, Age = 1, Active = true };

var client1 = new Client { Id = 0 ,Name = "Example", Gender=true, Pets=
    new List<Pet> { pet1 }, Active = true };
var client2 = new Client { Id = 0,
    Name = "Example2", Gender = false, Pets = new List<Pet> {
        pet2, pet3 }, Active = true };
var client3 = new Client { Id = 0,
    Name = "Example3", Gender = true, Pets = new List<Pet> {pet3 }, Active = true };





petService.AddPet(pet1);
petService.AddPet(pet2);
petService.AddPet(pet3);
petService.AddPet(pet4);

clientService.AddClient(client1);
client1 = clientService.GetClientById(1);
client1.Pets[0] = petService.GetPetById(1);
foreach (var pet in client1.Pets)
{
    int petid = 0;
    clientService.AddPetToClient(client1.Pets[petid], client1);
    petid++;
}


clientService.AddClient(client2);
client2 = clientService.GetClientById(2);
client2.Pets[0] = petService.GetPetById(2);
client2.Pets[1] = petService.GetPetById(3);
foreach (var pet in client2.Pets)
{
    int petid = 0;
    clientService.AddPetToClient(client2.Pets[petid],client2);
    petid++;
}


clientService.AddClient(client3);
client3 = clientService.GetClientById(3);
client3.Pets[0] = petService.GetPetById(4);
foreach (var pet in client3.Pets)
{
    int petid = 0;
    clientService.AddPetToClient(client3.Pets[petid], client3);
    petid++;
}

var clients = clientService.GetAllClients();
foreach (var client in clients)
{
    Console.WriteLine(client);
}
//adding pets to clients
/* if (clientService.AddPetToClient(pet1, client1)) // Adding pet to first client
        Console.WriteLine("Pet added to client successfully.");
    else
        Console.WriteLine("Failed to add pet to client.");
    if (clientService.AddPetToClient(pet2, client2)) // Adding pet to second client
        Console.WriteLine("Pet added to client successfully.");
    else
        Console.WriteLine("Failed to add pet to client.");
        if (clientService.AddPetToClient(pet3, client2)) // Adding another pet to second client
        Console.WriteLine("Pet added to client successfully.");
if (clientService.AddPetToClient(pet4,client3)) // Adding pet to third client
        Console.WriteLine("Pet added to client successfully.");
    else
        Console.WriteLine("Failed to add pet to client.");*/

//listing pets
var pets = petService.GetAllPets();
foreach (var pet in pets)
{
    Console.WriteLine(pet);
}

//listing clients with their pets
foreach (var client in clients)
{
    var detailedClient = clientService.GetClientById(client.Id);
    var petList = petService.GetPetsByClientId(client.Id);
    detailedClient.Pets = petList;
    Console.WriteLine(detailedClient);
}

