using TNT;

namespace MetaWearRPC
{
	/// <summary>
	/// Interface (contract) for RPC client-server interaction.
	/// </summary>
	public interface IMetaWearContract
	{
		/// <summary>
		/// Return a description of the MetaWearBoard with the given mac address.
		/// </summary>
		[TntMessage(1)]
		string GetBoardModel(ulong pMacAdress);

		/// <summary>
		/// Return the battery level of the MetaWearBoard with the given mac address.
		/// </summary>
		[TntMessage(2)]
		byte GetBatteryLevel(ulong pMacAdress);

        /// <summary>
        /// Start pulsing a motor on the MetaWearBoard with the given mac address.
        /// </summary>
        /// <param name="pMacAdress"></param>
        /// <param name="pDurationMs">How long to run the motor, in milliseconds (ms)</param>
        /// <param name="pIntensity">Strength of the motor [0.0f ; 100.0f]</param>
        [TntMessage(3)]
		void StartMotor(ulong pMacAdress, ushort pDurationMs, float pIntensity);

		/// <summary>
		/// Start pulsing a motor a defined number of times on the MetaWearBoard with the given mac address.
		/// </summary>
		/// <param name="pMacAdress"></param>
		/// <param name="pDurationMs">How long are the iterations of the motor pattern, in milliseconds (ms)</param>
		/// <param name="pIntensity">Strength of the motor pattern iterations [0.0f ; 100.0f]</param>
		/// <param name="pSleepMs">How long to wait between each iterations</param>
		/// <param name="pPatternIterations">The number of iterations</param>
		[TntMessage(4)]
		void StartMotorPattern(ulong pMacAdress, ushort pDurationMs, float pIntensity, ushort pSleepMs, int pPatternIterations);

        /// <summary>
        /// Start pulsing a buzzer on the MetaWearBoard with the given mac address.
        /// </summary>
        /// <param name="pMacAdress"></param>
        /// <param name="pDurationMs">How long to run the buzzer, in milliseconds (ms)</param>
        [TntMessage(5)]
		void StartBuzzer(ulong pMacAdress, ushort pDurationMs);

        /// <summary>
        /// Stop the led on the device
        /// <param name="pMacAdress"></param>
        /// </summary>
        [TntMessage(6)]
        void StopLED(ulong pMacAdress);

        /// <summary>
        /// Show a color on the LED on the device
		/// <param name="pMacAdress"></param>
		/// <param name="pColor">The color of the light, G=0, R=1, B=2 </param>
        /// </summary>
        [TntMessage(7)]
        void StartLED(ulong pMacAdress, ushort pColor);
    }
}
