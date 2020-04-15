using System;
using System.Collections;

namespace Prosimo {
   /// <summary>
   /// Summary description for StringConstants.
   /// </summary>
   public abstract class StringConstants {

      public const string INAPPROPRIATE_SPECIFIED_VALUE = "Inappropriate Specified Value";
      public const string INAPPROPRIATE_CALCULATED_VALUE = "Inappropriate Calculated Value";
      public const string OVER_SPECIFICATION = "Over Specification";
      public const string CALCULATION_FAILURE = "Calculation Failure";
      public const string SECIFIED_VALUE_CAUSING_OTHER_VARS_INAPPROPRIATE = "Specified variable value causes the specified values of some other variables to be inappropriate.";

      // labels
      public const string MASS_FLOW_RATE = "MASS_FLOW_RATE";
      public const string VOLUME_FLOW_RATE = "VOLUME_FLOW_RATE";
      public const string MOLE_FLOW_RATE = "MOLE_FLOW_RATE";
      public const string TEMPERATURE = "TEMPERATURE";
      public const string PRESSURE = "PRESSURE";
      public const string VAPOR_FRACTION = "VAPOR_FRACTION";
      public const string SPECIFIC_ENTHALPY = "SPECIFIC_ENTHALPY";
      public const string SPECIFIC_HEAT = "SPECIFIC_HEAT";
      public const string DENSITY = "DENSITY";
      public const string DYNAMIC_VISCOSITY = "DYNAMIC_VISCOSITY";
      public const string THERMAL_CONDUCTIVITY = "THERMAL_CONDUCTIVITY";

      public const string DRY_BULB_TEMPERATURE = "DRY_BULB_TEMPERATURE";
      public const string WET_BULB_TEMPERATURE = "WET_BULB_TEMPERATURE";
      public const string DEW_POINT_TEMPERATURE = "DEW_POINT_TEMPERATURE";
      public const string ABSOLUTE_HUMIDITY = "ABSOLUTE_HUMIDITY";
      public const string RELATIVE_HUMIDITY = "RELATIVE_HUMIDITY";
      public const string MASS_FLOW_RATE_DRY = "MASS_FLOW_RATE_DRY";
      public const string MOISTURE_CONTENT_DRY = "MOISTURE_CONTENT_DRY";
      public const string MOISTURE_CONTENT_WET = "MOISTURE_CONTENT_WET";

      //public const string SPECIFIC_VOLUME = "SPECIFIC_VOLUME";
      public const string SPECIFIC_HEAT_DRY = "SPECIFIC_HEAT_DRY";
      public const string SPECIFIC_ENTHALPY_DRY = "SPECIFIC_ENTHALPY_DRY";
      public const string SPECIFIC_HEAT_ABS_DRY = "SPECIFIC_HEAT_ABS_DRY";
      public const string HUMID_HEAT = "HUMID_HEAT";
      public const string HUMID_ENTHALPY = "HUMID_ENTHALPY";
      public const string HUMID_VOLUME = "HUMID_VOLUME";
      public const string MASS_FRACTION = "MASS_FRACTION";
      public const string MOLE_FRACTION = "MOLE_FRACTION";

      public const string MASS_CONCENTRATION = "MASS_CONCENTRATION";
      public const string SOLVENT_BOILING_POINT = "SOLVENT_BOILING_POINT";
      public const string SOLUTION_BOILING_POINT = "SOLUTION_BOILING_POINT";
      public const string INLET_DUST_LOADING = "INLET_DUST_LOADING";
      public const string OUTLET_DUST_LOADING = "OUTLET_DUST_LOADING";
      public const string DUST_ACCUMULATION_RATE = "DUST_ACCUMULATION_RATE";
      public const string FILTRATION_VELOCITY = "FILTRATION_VELOCITY";
      public const string TOTAL_FILTERING_AREA = "TOTAL_FILTERING_AREA";
      public const string BAG_DIAMETER = "BAG_DIAMETER";
      public const string BAG_LENGTH = "BAG_LENGTH";
      public const string NUMBER_OF_BAGS = "NUMBER_OF_BAGS";
      public const string DRIFT_VELOCITY = "DRIFT_VELOCITY";
      public const string TOTAL_SURFACE_AREA = "TOTAL_SURFACE_AREA";

      public const string NUMBER_OF_CYCLONES = "NUMBER_OF_CYCLONES";
      public const string INLET_WIDTH = "INLET_WIDTH";
      public const string INLET_HEIGHT = "INLET_HEIGHT";
      public const string INLET_HEIGHT_TO_WIDTH_RATIO = "INLET_HEIGHT_TO_WIDTH_RATIO";
      public const string CYCLONE_DIAMETER = "CYCLONE_DIAMETER";
      public const string INLET_VELOCITY = "INLET_VELOCITY";
      public const string OUTLET_VELOCITY = "OUTLET_VELOCITY";
      public const string OUTLET_INNER_DIAMETER = "OUTLET_INNER_DIAMETER";
      public const string OUTLET_WALL_THICKNESS = "OUTLET_WALL_THICKNESS";
      public const string OUTLET_TUBE_LENGTH_BELOW_ROOF = "OUTLET_TUBE_LENGTH_BELOW_ROOF";
      public const string OUTLET_BELOW_ROOF_TO_INLET_HEIGHT_RATIO = "OUTLET_BELOW_ROOF_TO_INLET_HEIGHT_RATIO";
      public const string NATURAL_VORTEX_LENGTH = "NATURAL_VORTEX_LENGTH";
      public const string DIPLEG_DIAMETER = "DIPLEG_DIAMETER";
      public const string EXTERNAL_VESSEL_DIAMETER = "EXTERNAL_VESSEL_DIAMETER";
      public const string CONE_ANGLE = "CONE_ANGLE";
      public const string BARREL_LENGTH = "BARREL_LENGTH";
      public const string CONE_LENGTH = "CONE_LENGTH";
      public const string BARREL_PLUS_CONE_LENGTH = "BARREL_PLUS_CONE_LENGTH";

      public const string CUT_PARTICLE_DIAMETER = "CUT_PARTICLE_DIAMETER";
      public const string INLET_PARTICLE_LOADING = "INLET_PARTICLE_LOADING";
      public const string OUTLET_PARTICLE_LOADING = "OUTLET_PARTICLE_LOADING";
      public const string PARTICLE_LOSS_TO_GAS_OUTLET = "PARTICLE_LOSS_TO_GAS_OUTLET";
      public const string PARTICLE_BULK_DENSITY = "PARTICLE_BULK_DENSITY";
      public const string PARTICLE_DENSITY = "PARTICLE_DENSITY";
      public const string COLLECTION_EFFICIENCY = "COLLECTION_EFFICIENCY";
      public const string PARTICLE_COLLECTION_RATE = "PARTICLE_COLLECTION_RATE";

      public const string PRESSURE_DROP = "PRESSURE_DROP";
      public const string POWER_INPUT = "POWER_INPUT";
      public const string CAPACITY = "CAPACITY";
      public const string DISCHARGE_FRICTION_HEAD = "DISCHARGE_FRICTION_HEAD";
      public const string EFFICIENCY = "EFFICIENCY";

      public const string STATIC_PRESSURE = "STATIC_PRESSURE";
      public const string STATIC_DICHARGE_HEAD = "STATIC_DICHARGE_HEAD";
      public const string STATIC_SUCTION_HEAD = "STATIC_SUCTION_HEAD";
      public const string SUCTION_FRICTION_HEAD = "SUCTION_FRICTION_HEAD";

      public const string TOTAL_DYNAMIC_HEAD = "TOTAL_DYNAMIC_HEAD";
      public const string TOTAL_DISCHARGE_PRESSURE = "TOTAL_DISCHARGE_PRESSURE";

      public const string LIQUID_GAS_RATIO = "LIQUID_GAS_RATIO";
      public const string LIQUID_RECIRCULATION_MASS_FLOW_RATE = "LIQUID_RECIRCULATION_MASS_FLOW_RATE";
      public const string LIQUID_RECIRCULATION_VOLUME_FLOW_RATE = "LIQUID_RECIRCULATION_VOLUME_FLOW_RATE";
      public const string LIQUID_GAS_VOLUME_RATIO = "LIQUID_GAS_VOLUME_RATIO";
      public const string ENTRAINMENT_RATIO = "ENTRAINMENT_RATIO";
      public const string COMPRESSION_RATIO = "COMPRESSION_RATIO";
      public const string SUCTION_MOTIVE_PRESSURE_RATIO = "SUCTION_MOTIVE_PRESSURE_RATIO";

      public const string GAS_PRESSURE_DROP = "GAS_PRESSURE_DROP";
      public const string GAS_INLET_BREADTH = "GAS_INLET_BREADTH";
      public const string GAS_INLET_HEIGHT = "GAS_INLET_HEIGHT";
      public const string GAS_OUTLET_DIAMETER = "GAS_OUTLET_DIAMETER";
      public const string GAS_OUTLET_PLUG_IN_HEIGHT = "GAS_OUTLET_PLUG_IN_HEIGHT";
      public const string DIAMETER = "DIAMETER";
      public const string HEIGHT = "HEIGHT";
      public const string LENGTH_DIAMETER_RATIO = "LENGTH_DIAMETER_RATIO";
      public const string WIDTH = "WIDTH";
      public const string LENGTH = "LENGTH";
      public const string LENGTH_WIDTH_RATIO = "LENGTH_WIDTH_RATIO";
      public const string HEIGHT_WIDTH_RATIO = "HEIGHT_WIDTH_RATIO";
      public const string GAS_VELOCITY = "GAS_VELOCITY";
      public const string VOLUME_HEAT_TRANSFER_COEFFICIENT = "VOLUME_HEAT_TRANSFER_COEFFICIENT";

      //public const string OPERATING_PRESSURE = "Operating Pressure";
      //public const string AVERAGE_PARTICLE_DIAMETER = "Average Particle Diameter";

      public const string HEAT_LOSS = "HEAT_LOSS";
      public const string HEAT_INPUT = "HEAT_INPUT";
      public const string HEATING_DUTY = "HEATING_DUTY";
      public const string COOLING_DUTY = "COOLING_DUTY";
      public const string FRACTION_OF_MATERIAL_LOST_TO_GAS_OUTLET = "FRACTION_OF_MATERIAL_LOST_TO_GAS_OUTLET";
      public const string GAS_OUTLET_MATERIAL_LOADING = "GAS_OUTLET_MATERIAL_LOADING";
      public const string HEAT_LOSS_BY_TRANSPORT_DEVICE = "HEAT_LOSS_BY_TRANSPORT_DEVICE";
      public const string MOISTURE_EVAPORATION_RATE = "MOISTURE_EVAPORATION_RATE";
      public const string THERMAL_EFFICIENCY = "THERMAL_EFFICIENCY";
      public const string SPECIFIC_HEAT_CONSUMPTION = "SPECIFIC_HEAT_CONSUMPTION";
      public const string INITIAL_GAS_TEMPERATURE = "INITIAL_GAS_TEMPERATURE";

      public const string OUTLET_DIAMETER = "OUTLET_DIAMETER";
      public const string INLET_DIAMETER = "INLET_DIAMETER";

      public const string FRACTION = "FRACTION";
      public const string WEIGHT_FRACTION = "WEIGHT_FRACTION";

      public const string WORK_INPUT = "WORK_INPUT";
      public const string PRESSURE_RATIO = "PRESSURE_RATIO";
      public const string POLYTROPIC_EFFICIENCY = "POLYTROPIC_EFFICIENCY";
      public const string ADIABATIC_EFFICIENCY = "ADIABATIC_EFFICIENCY";
      public const string POLYTROPIC_EXPONENT = "POLYTROPIC_EXPONENT";
      public const string ADIABATIC_EXPONENT = "ADIABATIC_EXPONENT";

      public const string COLD_INLET = "COLD_INLET";
      public const string COLD_OUTLET = "COLD_OUTLET";
      public const string HOT_INLET = "HOT_INLET";
      public const string HOT_OUTLET = "HOT_OUTLET";
      public const string COLD_SIDE_HEAT_TRANSFER_COEFFICIENT = "COLD_SIDE_HEAT_TRANSFER_COEFFICIENT";
      public const string HOT_SIDE_HEAT_TRANSFER_COEFFICIENT = "HOT_SIDE_HEAT_TRANSFER_COEFFICIENT";
      public const string TOTAL_HEAT_TRANSFER_COEFFICIENT = "TOTAL_HEAT_TRANSFER_COEFFICIENT";
      public const string TOTAL_HEAT_TRANSFER_AREA = "TOTAL_HEAT_TRANSFER_AREA";
      public const string TOTAL_HEAT_TRANSFER = "TOTAL_HEAT_TRANSFER";
      public const string COLD_SIDE_FOULING_FACTOR = "COLD_SIDE_FOULING_FACTOR";
      public const string HOT_SIDE_FOULING_FACTOR = "HOT_SIDE_FOULING_FACTOR";
      public const string COLD_SIDE_PRESSURE_DROP = "COLD_SIDE_PRESSURE_DROP";
      public const string HOT_SIDE_PRESSURE_DROP = "HOT_SIDE_PRESSURE_DROP";
      public const string NUMBER_OF_HEAT_TRANSFER_UNITS = "NUMBER_OF_HEAT_TRANSFER_UNITS";
      public const string EXCHANGER_EFFECTIVENESS = "EXCHANGER_EFFECTIVENESS";

      public const string HOT_SHELL_SIDE_PRESSURE_DROP = "Hot/Shell Side Pressure Drop";
      public const string COLD_TUBE_SIDE_PRESSURE_DROP = "Cold/Tube Side Pressure Drop";
      public const string HOT_TUBE_SIDE_PRESSURE_DROP = "Hot/Tube Side Pressure Drop";
      public const string COLD_SHELL_SIDE_PRESSURE_DROP = "Cold/Shell Side Pressure Drop";

      public const string TUBE_SIDE_INLET = "TUBE_SIDE_INLET";
      public const string TUBE_SIDE_OUTLET = "TUBE_SIDE_OUTLET";
      public const string SHELL_SIDE_INLET = "SHELL_SIDE_INLET";
      public const string SHELL_SIDE_OUTLET = "SHELL_SIDE_OUTLET";
      public const string TUBE_SIDE_HEAT_TRANSFER_COEFFICIENT = "TUBE_SIDE_HEAT_TRANSFER_COEFFICIENT";
      public const string SHELL_SIDE_HEAT_TRANSFER_COEFFICIENT = "SHELL_SIDE_HEAT_TRANSFER_COEFFICIENT";
      public const string TUBE_SIDE_FOULING_FACTOR = "TUBE_SIDE_FOULING_FACTOR";
      public const string SHELL_SIDE_FOULING_FACTOR = "SHELL_SIDE_FOULING_FACTOR";
      public const string TUBE_SIDE_PRESSURE_DROP = "TUBE_SIDE_PRESSURE_DROP";
      public const string SHELL_SIDE_PRESSURE_DROP = "SHELL_SIDE_PRESSURE_DROP";

      public const string CHANNEL_WIDTH = "CHANNEL_WIDTH";
      public const string PROJECTED_CHANNEL_LENGTH = "PROJECTED_CHANNEL_LENGTH";
      public const string PROJECTED_PLATE_AREA = "PROJECTED_PLATE_AREA";
      public const string ENLARGEMENT_FACTOR = "ENLARGEMENT_FACTOR";
      public const string ACTUAL_EFFECTIVE_PLATE_AREA = "ACTUAL_EFFECTIVE_PLATE_AREA";
      public const string PLATE_PITCH = "PLATE_PITCH";
      public const string PLATE_WALL_THICKNESS = "PLATE_WALL_THICKNESS";
      //public const string CHEVRON_ANGLE = "Chevron Angle";
      public const string NUMBER_OF_PLATES = "NUMBER_OF_PLATES";
      public const string NUMBER_OF_PASSES = "NUMBER_OF_PASSES";
      public const string HOT_SIDE_PASSES = "HOT_SIDE_PASSES";
      public const string COLD_SIDE_PASSES = "COLD_SIDE_PASSES";
      public const string PORT_DIAMETER = "PORT_DIAMETER";
      public const string HORIZONTAL_PORT_DISTANCE = "HORIZONTAL_PORT_DISTANCE";
      public const string VERTICAL_PORT_DISTANCE = "VERTICAL_PORT_DISTANCE";
      public const string COMPRESSED_PLATE_PACK_LENGTH = "COMPRESSED_PLATE_PACK_LENGTH";

      public const string SHELL_PASSES = "SHELL_PASSES";
      public const string TUBE_PASSES_PER_SHELL_PASS = "TUBE_PASSES_PER_SHELL_PASS";
      public const string TUBES_PER_TUBE_PASS = "TUBES_PER_TUBE_PASS";
      public const string TOTAL_TUBES_IN_SHELL = "TOTAL_TUBES_IN_SHELL";
      public const string TUBE_INNER_DIAMETER = "TUBE_INNER_DIAMETER";
      public const string TUBE_OUTER_DIAMETER = "TUBE_OUTER_DIAMETER";
      public const string TUBE_WALL_THICKNESS = "TUBE_WALL_THICKNESS";
      public const string TUBE_LENGTH = "TUBE_LENGTH";

      public const string TUBE_PITCH = "TUBE_PITCH";
      public const string TUBE_LAYOUT = "TUBE_LAYOUT";

      public const string NUMBER_OF_BAFFLES = "NUMBER_OF_BAFFLES";
      public const string BAFFLE_CUT = "BAFFLE_CUT";
      public const string BAFFLE_SPACING = "BAFFLE_SPACING";
      public const string ENTRANCE_BAFFLE_SPACING = "ENTRANCE_BAFFLE_SPACING";
      public const string EXIT_BAFFLE_SPACING = "EXIT_BAFFLE_SPACING";

      public const string SHELL_INNER_DIAMETER = "SHELL_INNER_DIAMETER";
      public const string BUNDLE_TO_SHELL_CLEARANCE = "BUNDLE_TO_SHELL_CLEARANCE";
      public const string SHELL_TO_BAFFLE_CLEARANCE = "SHELL_TO_BAFFLE_CLEARANCE";
      public const string SEALING_STRIPS = "SEALING_STRIPS";

      //      public const string HOT_SIDE_ENTRANCE_NOZZLE_DIAMETER = "Hot Side Entrance Nozzle Diameter";
      //      public const string HOT_SIDE_EXIT_NOZZLE_DIAMETER = "Hot Side Exit Nozzle Diameter";
      //      public const string COLD_SIDE_ENTRANCE_NOZZLE_DIAMETER = "Cold Side Entrance Nozzle Diameter";
      //      public const string COLD_SIDE_EXIT_NOZZLE_DIAMETER = "Cold Side Exit Nozzle Diameter";

      public const string TUBE_ENTRANCE_NOZZLE_DIAMETER = "TUBE_ENTRANCE_NOZZLE_DIAMETER";
      public const string TUBE_EXIT_NOZZLE_DIAMETER = "TUBE_EXIT_NOZZLE_DIAMETER";
      public const string SHELL_ENTRANCE_NOZZLE_DIAMETER = "SHELL_ENTRANCE_NOZZLE_DIAMETER";
      public const string SHELL_EXIT_NOZZLE_DIAMETER = "SHELL_EXIT_NOZZLE_DIAMETER";
      public const string FT_FACTOR = "FT_FACTOR";

      public const string HOT_SIDE_VELOCITY = "HOT_SIDE_VELOCITY";
      public const string COLD_SIDE_VELOCITY = "COLD_SIDE_VELOCITY";
      public const string HOT_SIDE_RE = "HOT_SIDE_RE";
      public const string COLD_SIDE_RE = "COLD_SIDE_RE";

      public const string SHELL_SIDE_VELOCITY = "SHELL_SIDE_VELOCITY";
      public const string SHELL_SIDE_RE = "SHELL_SIDE_RE";
      public const string TUBE_SIDE_VELOCITY = "TUBE_SIDE_VELOCITY";
      public const string TUBE_SIDE_RE = "TUBE_SIDE_RE";

      public const string WALL_THERMAL_CONDUCTIVITY = "WALL_THERMAL_CONDUCTIVITY";
      public const string WALL_THICKNESS = "WALL_THICKNESS";

      public const string ADIABATIC_SATURATION = "ADIABATIC_SATURATION";
      public const string EVAPORATION_HEAT = "EVAPORATION_HEAT";
      public const string SPECIFIC_VOLUME_DRY_AIR = "SPECIFIC_VOLUME_DRY_AIR";
      public const string SATURATION_VOLUME = "SATURATION_VOLUME";

      public const string TARGET_VALUE = "TARGET_VALUE";

      public const string EXCESS_AIR = "EXCESS_AIR";
      public const string HEAT_VALUE = "HEAT_VALUE";

      //////////////////////////////////////

      private static Hashtable varTypeNameTable = new Hashtable();

      static StringConstants() {
         varTypeNameTable.Add(MASS_FLOW_RATE, "Mass Flow Rate Wet Basis");
         varTypeNameTable.Add(VOLUME_FLOW_RATE, "Volume Flow Rate");
         varTypeNameTable.Add(MOLE_FLOW_RATE, "Mole Flow Rate");
         varTypeNameTable.Add(TEMPERATURE, "Temperature");
         varTypeNameTable.Add(PRESSURE, "Pressure");
         varTypeNameTable.Add(VAPOR_FRACTION, "Vapor Fraction");
         varTypeNameTable.Add(SPECIFIC_ENTHALPY, "Specific Enthalpy");
         varTypeNameTable.Add(SPECIFIC_HEAT, "Specific Heat");
         varTypeNameTable.Add(DENSITY, "Density");
         varTypeNameTable.Add(DYNAMIC_VISCOSITY, "Dynamic Viscosity");
         varTypeNameTable.Add(THERMAL_CONDUCTIVITY, "Thermal Conductivity");

         varTypeNameTable.Add(DRY_BULB_TEMPERATURE, "Dry-bulb Temperature");
         varTypeNameTable.Add(WET_BULB_TEMPERATURE, "Wet-bulb Temperature");
         varTypeNameTable.Add(DEW_POINT_TEMPERATURE, "Dew Point Temperature");
         varTypeNameTable.Add(ABSOLUTE_HUMIDITY, "Absolute Humidity");
         varTypeNameTable.Add(RELATIVE_HUMIDITY, "Relative Humidity");
         varTypeNameTable.Add(MASS_FLOW_RATE_DRY, "Mass Flow Rate Dry Basis");
         varTypeNameTable.Add(MOISTURE_CONTENT_DRY, "Moisture Content Dry Basis");
         varTypeNameTable.Add(MOISTURE_CONTENT_WET, "Moisture Content Wet Basis");

         //varTypeNameTable.Add(SPECIFIC_VOLUME, "Specific Volume");
         varTypeNameTable.Add(SPECIFIC_HEAT_DRY, "Specific Heat Dry Basis");
         varTypeNameTable.Add(SPECIFIC_ENTHALPY_DRY, "Specific Enthalpy Dry Basis");
         varTypeNameTable.Add(SPECIFIC_HEAT_ABS_DRY, "Specific Heat of Bone Dry Material");
         varTypeNameTable.Add(HUMID_HEAT, "Humid Heat");
         varTypeNameTable.Add(HUMID_ENTHALPY, "Humid Enthalpy");
         varTypeNameTable.Add(HUMID_VOLUME, "Humid Volume");
         varTypeNameTable.Add(MASS_FRACTION, "Mass Fraction");
         varTypeNameTable.Add(MOLE_FRACTION, "Mole Fraction");

         varTypeNameTable.Add(MASS_CONCENTRATION, "Mass Concentration");
         varTypeNameTable.Add(SOLVENT_BOILING_POINT, "Solvent Boiling Point");
         varTypeNameTable.Add(SOLUTION_BOILING_POINT, "Solution Boiling Point");
         varTypeNameTable.Add(INLET_DUST_LOADING, "Inlet Dust Loading");
         varTypeNameTable.Add(OUTLET_DUST_LOADING, "Outlet Dust Loading");
         varTypeNameTable.Add(DUST_ACCUMULATION_RATE, "Dust Accumulation Rate");
         varTypeNameTable.Add(FILTRATION_VELOCITY, "Filtration Velocity");
         varTypeNameTable.Add(TOTAL_FILTERING_AREA, "Total Filtering Area");
         varTypeNameTable.Add(BAG_DIAMETER, "Bag Diameter");
         varTypeNameTable.Add(BAG_LENGTH, "Bag Length");
         varTypeNameTable.Add(NUMBER_OF_BAGS, "Number of Bags");
         varTypeNameTable.Add(DRIFT_VELOCITY, "Drift Velocity");
         varTypeNameTable.Add(TOTAL_SURFACE_AREA, "Total Surface Area");

         varTypeNameTable.Add(NUMBER_OF_CYCLONES, "Number of Cyclones");
         varTypeNameTable.Add(INLET_WIDTH, "Inlet Width");
         varTypeNameTable.Add(INLET_HEIGHT, "Inlet Height");
         varTypeNameTable.Add(INLET_HEIGHT_TO_WIDTH_RATIO, "Inlet Height/Width Ratio");
         varTypeNameTable.Add(CYCLONE_DIAMETER, "Cyclone Diameter");
         varTypeNameTable.Add(INLET_VELOCITY, "Inlet Velocity");
         varTypeNameTable.Add(OUTLET_VELOCITY, "Outlet Velocity");
         varTypeNameTable.Add(OUTLET_INNER_DIAMETER, "Outlet Inner Diameter");
         varTypeNameTable.Add(OUTLET_WALL_THICKNESS, "Outlet Wall Thickness");
         varTypeNameTable.Add(OUTLET_TUBE_LENGTH_BELOW_ROOF, "Outlet Tube Length Below Roof");
         varTypeNameTable.Add(OUTLET_BELOW_ROOF_TO_INLET_HEIGHT_RATIO, "Outlet Below Roof/Inlet Height Ratio");
         varTypeNameTable.Add(NATURAL_VORTEX_LENGTH, "Natural Vortex Length");
         varTypeNameTable.Add(DIPLEG_DIAMETER, "Dipleg Diameter");
         varTypeNameTable.Add(EXTERNAL_VESSEL_DIAMETER, "External Vessel Diameter");
         varTypeNameTable.Add(CONE_ANGLE, "Cone Angle");
         varTypeNameTable.Add(BARREL_LENGTH, "Barrel Length");
         varTypeNameTable.Add(CONE_LENGTH, "Cone Length");
         varTypeNameTable.Add(BARREL_PLUS_CONE_LENGTH, "Barrel Plus Cone Length");

         varTypeNameTable.Add(CUT_PARTICLE_DIAMETER, "Cut Particle Diameter");
         varTypeNameTable.Add(INLET_PARTICLE_LOADING, "Inlet Particle Loading");
         varTypeNameTable.Add(OUTLET_PARTICLE_LOADING, "Outlet Particle Loading");
         varTypeNameTable.Add(PARTICLE_LOSS_TO_GAS_OUTLET, "Particle Loss to Gas Outlet");
         varTypeNameTable.Add(PARTICLE_BULK_DENSITY, "Particle Bulk Density");
         varTypeNameTable.Add(PARTICLE_DENSITY, "Particle Density");
         varTypeNameTable.Add(COLLECTION_EFFICIENCY, "Collection Efficiency");
         varTypeNameTable.Add(PARTICLE_COLLECTION_RATE, "Particle Collection Rate");

         varTypeNameTable.Add(PRESSURE_DROP, "Pressure Drop");
         varTypeNameTable.Add(POWER_INPUT, "Power Input");
         varTypeNameTable.Add(CAPACITY, "Capacity");
         varTypeNameTable.Add(DISCHARGE_FRICTION_HEAD, "Discharge Friction Head");
         varTypeNameTable.Add(EFFICIENCY, "Efficiency");

         varTypeNameTable.Add(STATIC_PRESSURE, "Static Pressure");
         varTypeNameTable.Add(STATIC_DICHARGE_HEAD, "Static Discharge Head");
         varTypeNameTable.Add(STATIC_SUCTION_HEAD, "Static Suction Head");
         varTypeNameTable.Add(SUCTION_FRICTION_HEAD, "Suction Friction Head");

         varTypeNameTable.Add(TOTAL_DYNAMIC_HEAD, "Total Dynamic Head");
         varTypeNameTable.Add(TOTAL_DISCHARGE_PRESSURE, "Total Discharge Pressure");

         varTypeNameTable.Add(LIQUID_GAS_RATIO, "Liquid Gas Ratio");
         varTypeNameTable.Add(LIQUID_RECIRCULATION_MASS_FLOW_RATE, "Liquid Recirc. Mass Flow");
         varTypeNameTable.Add(LIQUID_RECIRCULATION_VOLUME_FLOW_RATE, "Liquid Recirc. Volume Flow");
         varTypeNameTable.Add(LIQUID_GAS_VOLUME_RATIO, "Liquid Gas Volume Ratio");
         varTypeNameTable.Add(ENTRAINMENT_RATIO, "Entrainment Ratio");
         varTypeNameTable.Add(COMPRESSION_RATIO, "Compression Ratio");
         varTypeNameTable.Add(SUCTION_MOTIVE_PRESSURE_RATIO, "Suction/Motive Pressure Ratio");

         varTypeNameTable.Add(GAS_PRESSURE_DROP, "Gas Pressure Drop");
         varTypeNameTable.Add(GAS_INLET_BREADTH, "Gas Inlet Breadth");
         varTypeNameTable.Add(GAS_INLET_HEIGHT, "Gas Inlet Height");
         varTypeNameTable.Add(GAS_OUTLET_DIAMETER, "Gas Outlet Diameter");
         varTypeNameTable.Add(GAS_OUTLET_PLUG_IN_HEIGHT, "Gas Outlet Plug-In Height");
         varTypeNameTable.Add(DIAMETER, "Diameter");
         varTypeNameTable.Add(HEIGHT, "Height");
         varTypeNameTable.Add(LENGTH_DIAMETER_RATIO, "Length/Diameter Ratio");
         varTypeNameTable.Add(WIDTH, "Width");
         varTypeNameTable.Add(LENGTH, "Length");
         varTypeNameTable.Add(LENGTH_WIDTH_RATIO, "Length/Width Ratio");
         varTypeNameTable.Add(HEIGHT_WIDTH_RATIO, "Height/Width Ratio");
         varTypeNameTable.Add(GAS_VELOCITY, "Gas Velocity");
         varTypeNameTable.Add(VOLUME_HEAT_TRANSFER_COEFFICIENT, "Volume Heat Transfer Coef");

         //varTypeNameTable.Add(OPERATING_PRESSURE, "Operating Pressure");
         //varTypeNameTable.Add(AVERAGE_PARTICLE_DIAMETER, "Average Particle Diameter");

         varTypeNameTable.Add(HEAT_LOSS, "Heat Loss");
         varTypeNameTable.Add(HEAT_INPUT, "Heat Input");
         varTypeNameTable.Add(HEATING_DUTY, "Heating Duty");
         varTypeNameTable.Add(COOLING_DUTY, "Cooling Duty");
         varTypeNameTable.Add(FRACTION_OF_MATERIAL_LOST_TO_GAS_OUTLET, "Dust Entrained in Gas/Material Total");
         varTypeNameTable.Add(GAS_OUTLET_MATERIAL_LOADING, "Gas Outlet Dust Loading");
         varTypeNameTable.Add(HEAT_LOSS_BY_TRANSPORT_DEVICE, "Heat Loss by Transport Device");
         varTypeNameTable.Add(MOISTURE_EVAPORATION_RATE, "Moisture Evaporation Rate");
         varTypeNameTable.Add(THERMAL_EFFICIENCY, "Thermal Efficiency");
         varTypeNameTable.Add(SPECIFIC_HEAT_CONSUMPTION, "Specific Heat Consumption");
         varTypeNameTable.Add(INITIAL_GAS_TEMPERATURE, "Initial Gas Temperature");

         varTypeNameTable.Add(OUTLET_DIAMETER, "Outlet Diameter");
         varTypeNameTable.Add(INLET_DIAMETER, "Inlet Diameter");

         varTypeNameTable.Add(FRACTION, "Fraction");
         varTypeNameTable.Add(WEIGHT_FRACTION, "Weight Fraction");

         varTypeNameTable.Add(WORK_INPUT, "Work Input");
         varTypeNameTable.Add(PRESSURE_RATIO, "Pressure Ratio");
         varTypeNameTable.Add(POLYTROPIC_EFFICIENCY, "Polytropic Efficiency");
         varTypeNameTable.Add(ADIABATIC_EFFICIENCY, "Adiabatic Efficiency");
         varTypeNameTable.Add(POLYTROPIC_EXPONENT, "Polytropic Exponent");
         varTypeNameTable.Add(ADIABATIC_EXPONENT, "Adiabatic Exponent");

         varTypeNameTable.Add(COLD_INLET, "Cold Side Inlet");
         varTypeNameTable.Add(COLD_OUTLET, "Cold Side Outlet");
         varTypeNameTable.Add(HOT_INLET, "Hot Side Inlet");
         varTypeNameTable.Add(HOT_OUTLET, "Hot Side Outlet");
         varTypeNameTable.Add(COLD_SIDE_HEAT_TRANSFER_COEFFICIENT, "Cold Side Heat Transfer Coef");
         varTypeNameTable.Add(HOT_SIDE_HEAT_TRANSFER_COEFFICIENT, "Hot Side Heat Transfer Coef");
         varTypeNameTable.Add(TOTAL_HEAT_TRANSFER_COEFFICIENT, "Total Heat Transfer Coef");
         varTypeNameTable.Add(TOTAL_HEAT_TRANSFER_AREA, "Total Heat Transfer Area");
         varTypeNameTable.Add(TOTAL_HEAT_TRANSFER, "Total Heat Transfer");
         varTypeNameTable.Add(COLD_SIDE_FOULING_FACTOR, "Cold Side Fouling Factor");
         varTypeNameTable.Add(HOT_SIDE_FOULING_FACTOR, "Hot Side Fouling Factor");
         varTypeNameTable.Add(COLD_SIDE_PRESSURE_DROP, "Cold Side Pressure Drop");
         varTypeNameTable.Add(HOT_SIDE_PRESSURE_DROP, "Hot Side Pressure Drop");
         varTypeNameTable.Add(NUMBER_OF_HEAT_TRANSFER_UNITS, "Number Of Heat Transfer Units");
         varTypeNameTable.Add(EXCHANGER_EFFECTIVENESS, "Exchanger Effectiveness");
         varTypeNameTable.Add(HOT_SHELL_SIDE_PRESSURE_DROP, "Hot/Shell Side Pressure Drop");
         varTypeNameTable.Add(COLD_TUBE_SIDE_PRESSURE_DROP, "Cold/Tube Side Pressure Drop");
         varTypeNameTable.Add(HOT_TUBE_SIDE_PRESSURE_DROP, "Hot/Tube Side Pressure Drop");
         varTypeNameTable.Add(COLD_SHELL_SIDE_PRESSURE_DROP, "Cold/Shell Side Pressure Drop");

         varTypeNameTable.Add(TUBE_SIDE_INLET, "Tube Inlet");
         varTypeNameTable.Add(TUBE_SIDE_OUTLET, "Tube Outlet");
         varTypeNameTable.Add(SHELL_SIDE_INLET, "Shell Inlet");
         varTypeNameTable.Add(SHELL_SIDE_OUTLET, "Shell Outlet");
         varTypeNameTable.Add(TUBE_SIDE_HEAT_TRANSFER_COEFFICIENT, "Tube Side Heat Transfer Coef");
         varTypeNameTable.Add(SHELL_SIDE_HEAT_TRANSFER_COEFFICIENT, "Shell Side Heat Transfer Coef");
         varTypeNameTable.Add(TUBE_SIDE_FOULING_FACTOR, "Tube Side Fouling Factor");
         varTypeNameTable.Add(SHELL_SIDE_FOULING_FACTOR, "Shell Side Fouling Factor");
         varTypeNameTable.Add(TUBE_SIDE_PRESSURE_DROP, "Tube Side Pressure Drop");
         varTypeNameTable.Add(SHELL_SIDE_PRESSURE_DROP, "Shell Side Pressure Drop");

         varTypeNameTable.Add(CHANNEL_WIDTH, "Channel Width");
         varTypeNameTable.Add(PROJECTED_CHANNEL_LENGTH, "Projected Channel Length");
         varTypeNameTable.Add(PROJECTED_PLATE_AREA, "Projected Plate Area");
         varTypeNameTable.Add(ENLARGEMENT_FACTOR, "Enlargement Factor");
         varTypeNameTable.Add(ACTUAL_EFFECTIVE_PLATE_AREA, "Actual Effective Plate Area");
         varTypeNameTable.Add(PLATE_PITCH, "Plate Pitch");
         varTypeNameTable.Add(PLATE_WALL_THICKNESS, "Plate Wall Thickness");
         //varTypeNameTable.Add(CHEVRON_ANGLE, "Chevron Angle");
         varTypeNameTable.Add(NUMBER_OF_PLATES, "Number Of Plates");
         varTypeNameTable.Add(NUMBER_OF_PASSES, "Number Of Passes");
         varTypeNameTable.Add(HOT_SIDE_PASSES, "Hot Side Passes");
         varTypeNameTable.Add(COLD_SIDE_PASSES, "Cold Side Passes");
         varTypeNameTable.Add(PORT_DIAMETER, "Port Diameter");
         varTypeNameTable.Add(HORIZONTAL_PORT_DISTANCE, "Horizontal Port Distance");
         varTypeNameTable.Add(VERTICAL_PORT_DISTANCE, "Vertical Port Distance");
         varTypeNameTable.Add(COMPRESSED_PLATE_PACK_LENGTH, "Compressed Plate Pack Length");

         varTypeNameTable.Add(SHELL_PASSES, "Shell Passes");
         varTypeNameTable.Add(TUBE_PASSES_PER_SHELL_PASS, "Tube Passes/Shell Pass");
         varTypeNameTable.Add(TUBES_PER_TUBE_PASS, "Tubes/Tube Pass");
         varTypeNameTable.Add(TOTAL_TUBES_IN_SHELL, "Total Tubes in Shell");
         varTypeNameTable.Add(TUBE_INNER_DIAMETER, "Tube Inner Diameter");
         varTypeNameTable.Add(TUBE_OUTER_DIAMETER, "Tube Outer Diameter");
         varTypeNameTable.Add(TUBE_WALL_THICKNESS, "Tube Wall Thickness");
         varTypeNameTable.Add(TUBE_LENGTH, "Tube Length");

         varTypeNameTable.Add(TUBE_PITCH, "Tube Pitch");
         varTypeNameTable.Add(TUBE_LAYOUT, "Tube Layout");

         varTypeNameTable.Add(NUMBER_OF_BAFFLES, "Number Of Baffles");
         varTypeNameTable.Add(BAFFLE_CUT, "Baffle Cut");
         varTypeNameTable.Add(BAFFLE_SPACING, "Baffle Spacing");
         varTypeNameTable.Add(ENTRANCE_BAFFLE_SPACING, "Entrance Baffle Spacing");
         varTypeNameTable.Add(EXIT_BAFFLE_SPACING, "Exit Baffle Spacing");

         varTypeNameTable.Add(SHELL_INNER_DIAMETER, "Shell Inner Diameter");
         varTypeNameTable.Add(BUNDLE_TO_SHELL_CLEARANCE, "Bundle to Shell Clearance");
         varTypeNameTable.Add(SHELL_TO_BAFFLE_CLEARANCE, "Shell to Baffle Clearance");
         varTypeNameTable.Add(SEALING_STRIPS, "Sealing Strips");

         //      varTypeNameTable.Add(HOT_SIDE_ENTRANCE_NOZZLE_DIAMETER, "Hot Side Entrance Nozzle Diameter");
         //      varTypeNameTable.Add(HOT_SIDE_EXIT_NOZZLE_DIAMETER, "Hot Side Exit Nozzle Diameter");
         //      varTypeNameTable.Add(COLD_SIDE_ENTRANCE_NOZZLE_DIAMETER, "Cold Side Entrance Nozzle Diameter");
         //      varTypeNameTable.Add(COLD_SIDE_EXIT_NOZZLE_DIAMETER, "Cold Side Exit Nozzle Diameter");

         varTypeNameTable.Add(TUBE_ENTRANCE_NOZZLE_DIAMETER, "Tube Entrance Nozzle Diameter");
         varTypeNameTable.Add(TUBE_EXIT_NOZZLE_DIAMETER, "Tube Exit Nozzle Diameter");
         varTypeNameTable.Add(SHELL_ENTRANCE_NOZZLE_DIAMETER, "Shell Entrance Nozzle Diameter");
         varTypeNameTable.Add(SHELL_EXIT_NOZZLE_DIAMETER, "Shell Exit Nozzle Diameter");
         varTypeNameTable.Add(FT_FACTOR, "Ft Factor");

         varTypeNameTable.Add(HOT_SIDE_VELOCITY, "Hot Side Velocity");
         varTypeNameTable.Add(COLD_SIDE_VELOCITY, "Cold Side Velocity");
         varTypeNameTable.Add(HOT_SIDE_RE, "Hot Side Re");
         varTypeNameTable.Add(COLD_SIDE_RE, "Cold Side Re");

         varTypeNameTable.Add(SHELL_SIDE_VELOCITY, "Shell Side Velocity");
         varTypeNameTable.Add(SHELL_SIDE_RE, "Shell Side Re");
         varTypeNameTable.Add(TUBE_SIDE_VELOCITY, "Tube Side Velocity");
         varTypeNameTable.Add(TUBE_SIDE_RE, "Tube Side Re");

         varTypeNameTable.Add(WALL_THERMAL_CONDUCTIVITY, "Wall Thermal Conductivity");
         varTypeNameTable.Add(WALL_THICKNESS, "Wall Thickness");

         varTypeNameTable.Add(ADIABATIC_SATURATION, "Adiabatic Saturation");
         varTypeNameTable.Add(EVAPORATION_HEAT, "Evaporation Heat");
         varTypeNameTable.Add(SPECIFIC_VOLUME_DRY_AIR, "Specific Volume Dry Air");
         varTypeNameTable.Add(SATURATION_VOLUME, "Saturation Volume");

         varTypeNameTable.Add(TARGET_VALUE, "Target Value");
         varTypeNameTable.Add(EXCESS_AIR, "Excess Air");
         varTypeNameTable.Add(HEAT_VALUE, "Heat Value");
      }

      public static string GetTypeName(string label) {
         return (string)varTypeNameTable[label];
      }
   }
}