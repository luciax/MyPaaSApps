<?php
// require the predis library to talk to Redis (PHP)
require "Predis/Autoloader.php";
Predis\Autoloader::register();

//Setup to grab the VCAP service variables to connect to the database
$services = getenv("VCAP_SERVICES");
if (!$services == null)
{

        $services_json = json_decode($services,true);
		if ($redis_config = $services_json['redis-2.2'][0]["credentials"])
	{
		$REDIS_HOST=$redis_config["host"];
		$REDIS_PORT=$redis_config["port"];
		$REDIS_DATABASE=$redis_config["name"];
		$REDIS_PASSWORD=$redis_config["password"];
	}
	Else
	{
		echo 'Unable to get VCAP Services.';
	}
}

try 
	{
		//Instantiate new instance
   		//$redis = new Predis\Client();
  		$redis = new Predis\Client(array(
       		"scheme" => "tcp",
       		"host" => $REDIS_HOST,
       		"name" => $REDIS_DATABASE,
       		"password" => $REDIS_PASSWORD,
       		"port" => $REDIS_PORT));

		//Connect using above credentials	
    		$redis->connect();

    		echo "Successfully connected to Redis on " . $REDIS_HOST . "<br>";

		$key = "key";
		$value = "value";
		//Set a key-value pair (uncomment to set, recomment to test)
    		//$redis->set($key,$value);

    		$dbvalue = $redis->get($key);
    		echo "key: $key, value:" . $dbvalue . "<br>";
	}
	catch (Exception $e) 
	{
    		echo "Couldn't connect to Redis on " . $REDIS_HOST; 
    		echo $e->getMessage();
	}

?>
