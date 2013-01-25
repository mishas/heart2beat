using System;
using System.Collections.Generic;

public class Pair<T> {
	public Pair (T first, T second) {
		this.first = first;
		this.second = second;
	}
	
	public T first;
	public T second;
}
