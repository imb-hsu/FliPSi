# Flexible Production Simulation (FliPSi)

This is a simulation, created in Unity, aimed at data generation for machine learning applications in the domain of production systems. It is currently in an unstable alpha state and changes are likely to be made. The goal of the project is to create a tool for the generation of example data, and explicitly not the realistic simulation of existing production systems, meaning it is not suitable as a digital twin.  

## The Simulation
The simulation runs on Unity 2020.3.26.f1. 

## Publications

Currently, FliPSi has been featured in the following papers:

M. Krantz, et al. "Flipsi: Generating data for the training of machine learning algorithms for cpps." Annual Conference of the PHM Society. Vol. 14. No. 1. 2022., doi: 10.36001/phmconf.2022.v14i1.3229 
A. Liebert et al., "Using FliPSi to Generate Data for Machine Learning Algorithms," 2023 IEEE 28th International Conference on Emerging Technologies and Factory Automation (ETFA), Sinaia, Romania, 2023, pp. 1-8, doi: 10.1109/ETFA54631.2023.10275500.

## How to Start

Add the project in UnityHub. If necessary, install the required Unity version. Once the project is loaded, open the scene under: Assets/Scenes/FliPSiScene . Loading the project for the first time takes longer as not all files are stored in git. The simluation can be started using the play button. It is also possible to use the simulation outside of Unity once the project was coompiled. Given the early development stage, access through Unity is still preferred. 

A production system can be built using the on screen menu, during runtime. Data export can also be started using the menu system. Factories configurations can be stored and loaded. Example factories are provided in the top level folder (Sort_by_Color_A.xml etc.). 
