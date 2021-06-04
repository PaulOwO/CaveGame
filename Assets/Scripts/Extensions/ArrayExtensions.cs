public static class ArrayExtensions 
{
    public static void Fill<T>(this T[] originalArray, T value) {
        for(int i = 0; i < originalArray.Length; i++){
            originalArray[i] = value;
        }
    }  
}