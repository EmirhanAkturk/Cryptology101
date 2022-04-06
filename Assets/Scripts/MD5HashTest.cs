using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = System.Random;

public class MD5HashTest : MonoBehaviour
{
    private const string Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed pulvinar, nisl mollis sollicitudin faucibus, lacus odio tincidunt felis, quis efficitur mauris orci eu lorem. Phasellus tempor turpis mi, eu fermentum massa suscipit sit amet. Aenean ac posuere magna. Etiam pretium sem sed tempor interdum. Fusce non tincidunt sapien. Vestibulum ullamcorper, nulla commodo congue elementum, neque mi scelerisque tellus, malesuada sodales sem mauris eget felis. Ut vel lectus ex. Duis quis magna in egestas."; 
    
    private void Start()
    {
        HashTest();
    }

    private void HashTest()
    {
        var realHash = Hash.HashMD5(Message);
        Debug.Log($"Message : {Message}\nReal Hash : {realHash}");

        for (var i = 0; i < Message.Length; ++i)
        {
            var rndChar = GetDiffChar(Message[i]);
            var newString = new StringBuilder(Message);
            newString[i] = rndChar;
            Debug.Log($"{(i + 1)}. char : {Message[i]} => {rndChar}\n New Hash : {Hash.HashMD5(newString.ToString())}");
        }
    }

    private Random random;
    
    private char GetDiffChar(char c)
    {
        random ??= new Random();
        
        char rndC;
        do
        {
            rndC = (char) random.Next('a', 'z');
        } while (rndC.Equals(c));

        return rndC;
    }
}
