namespace Plugin.BluetoothLE.Abstractions
{
    public enum GattProperty
    {
        //
        // Summary:
        //     Characteristic proprty: Characteristic is broadcastable.
        //
        // Remarks:
        //     Portions of this page are modifications based on work created and shared by the
        //     Android Open Source Project and used according to terms described in the Creative
        //     Commons 2.5 Attribution License.
        Broadcast = 1,
        //
        // Summary:
        //     Characteristic property: Characteristic is readable.
        //
        // Remarks:
        //     Portions of this page are modifications based on work created and shared by the
        //     Android Open Source Project and used according to terms described in the Creative
        //     Commons 2.5 Attribution License.
        Read = 2,
        //
        // Summary:
        //     Characteristic property: Characteristic can be written without response.
        //
        // Remarks:
        //     Portions of this page are modifications based on work created and shared by the
        //     Android Open Source Project and used according to terms described in the Creative
        //     Commons 2.5 Attribution License.
        WriteNoResponse = 4,
        //
        // Summary:
        //     Characteristic property: Characteristic can be written.
        //
        // Remarks:
        //     Portions of this page are modifications based on work created and shared by the
        //     Android Open Source Project and used according to terms described in the Creative
        //     Commons 2.5 Attribution License.
        Write = 8,
        //
        // Summary:
        //     To be added.
        //
        // Remarks:
        //     Portions of this page are modifications based on work created and shared by the
        //     Android Open Source Project and used according to terms described in the Creative
        //     Commons 2.5 Attribution License.
        Notify = 16,
        //
        // Summary:
        //     To be added.
        //
        // Remarks:
        //     Portions of this page are modifications based on work created and shared by the
        //     Android Open Source Project and used according to terms described in the Creative
        //     Commons 2.5 Attribution License.
        Indicate = 32,
        //
        // Summary:
        //     To be added.
        //
        // Remarks:
        //     Portions of this page are modifications based on work created and shared by the
        //     Android Open Source Project and used according to terms described in the Creative
        //     Commons 2.5 Attribution License.
        SignedWrite = 64,
        //
        // Summary:
        //     To be added.
        //
        // Remarks:
        //     Portions of this page are modifications based on work created and shared by the
        //     Android Open Source Project and used according to terms described in the Creative
        //     Commons 2.5 Attribution License.
        ExtendedProps = 128
    }
}
