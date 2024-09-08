using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class ConnectDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        connect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void connect(){
        string connectionString ="server=localhost;port=3306;database=alpha;user=root;password=binh123456";
        using (MySqlConnection connection = new MySqlConnection(connectionString)){
            connection.Open();
            string query="SELECT * FROM alpha.chest_equipment limit 1";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()){
                int itemId=reader.GetInt32(0);
                int itemId2=reader.GetInt32(1);
                int itemId3=reader.GetInt32(2);
                Debug.Log("Connected to database "+itemId +itemId2+itemId3);
            }
            connection.Close();
        }
    }
}
