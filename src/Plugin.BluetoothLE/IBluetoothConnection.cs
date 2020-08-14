﻿namespace Plugin.BluetoothLE.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    ///<summary>
    /// The interface that represents a short time connection between a current bluetooth adapter and the remote bluetooth device.
    /// It can be used for the short time connections, when required to transmit/recieve a some small portion of the data and then close a connection.
    ///</summary>
    public interface IBluetoothConnection : IDisposable
    {
        /// <summary>
        /// The method that connects to the remote bluetooth device.
        /// </summary>
        /// <param name="autoConnect">The parameter that represents whether to directly connect to the remote device (false) 
        /// or to automatically connect as soon as the remote device becomes available (true).
        /// </param>
        void Connect(bool autoConnect = false);

        /// <summary>
        /// The property that represents an <see cref="IEnumerable{BluetoothServiceModel}"/> instance which is a remote device supported servcies.
        /// </summary>
        IEnumerable<BluetoothServiceModel> Services { get; }

        /// <summary>
        /// The method that transmit data to the remote bluetooth device asynchronously.
        /// </summary>
        /// <param name="characteristicModel">The parameter that represents a characteristics which value to transmit.</param>
        /// <param name="buffer">The parameter that represents a <see cref="Memory{byte}"/> buffer to transmit.</param>
        /// <returns>Returns a <see cref="bool"/> value.</returns>
        bool Transmit(BluetoothGattCharacteristicModel characteristicModel, Memory<byte> buffer);

        /// <summary>
        /// The method that transmit data to the remote bluetooth device asynchronously.
        /// </summary>
        /// <param name="characteristicModel">The parameter that represents a characteristics which value to transmit.</param>
        /// <param name="buffer">The parameter that represents a <see cref="byte[]"/> buffer to transmit.</param>
        /// <returns>Returns a <see cref="bool"/> value.</returns>
        bool Transmit(BluetoothGattCharacteristicModel characteristicModel, byte[] buffer);


        /// <summary>
        /// The property that represents is any data available to recive.
        /// </summary>
        bool DataAvailable { get; }

        /// <summary>
        /// The method that recive data from the remote bluetooth device asynchronously.
        /// </summary>
        /// <param name="buffer">The parameter that represents a <see cref="Memory{byte}"/> buffer to recive.</param>
        /// <param name="cancellationToken">The parameter that represents a <see cref="CancellationToken"/> instance.</param>
        /// <returns>Returns a <see cref="Task{int}"/> instance with a count of the recieved bytes.</returns>
        Task<int> ReciveAsync(Memory<byte> buffer, CancellationToken cancellationToken = default);

        /// <summary>
        /// The method that recive data from the remote bluetooth device asynchronously.
        /// </summary>
        /// <param name="buffer">The parameter that represents a <see cref="byte[]"/> buffer to recive.</param>
        /// <param name="offset">The parameter that represents a recive buffer offset.</param>
        /// <param name="count">The parameter that represents a count of bytes to recive.</param>
        /// <returns>Returns a <see cref="Task{int}"/> instance with a count of the recieved bytes.</returns>
        Task<int> ReciveAsync(byte[] buffer, int offset, int count);

        /// <summary>
        /// The method that recive data from the remote bluetooth device asynchronously.
        /// </summary>
        /// <param name="buffer">The parameter that represents a <see cref="byte[]"/> buffer to recive.</param>
        /// <param name="offset">The parameter that represents a recive buffer offset.</param>
        /// <param name="count">The parameter that represents a count of bytes to recive.</param>
        /// <param name="cancellationToken">The parameter that represents a <see cref="CancellationToken"/> instance.</param>
        /// <returns>Returns a <see cref="Task{int}"/> instance witch a count of the recived bytes.</returns>
        Task<int> ReciveAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken);


    }
}