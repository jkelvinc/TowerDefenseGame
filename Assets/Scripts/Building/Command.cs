using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface ICommand 
{
	event Action<string> OnCommandExecuted;
}
