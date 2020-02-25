// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#include "InventoryTestGameMode.h"
#include "InventoryTestHUD.h"
#include "InventoryTestCharacter.h"
#include "UObject/ConstructorHelpers.h"

AInventoryTestGameMode::AInventoryTestGameMode()
	: Super()
{
	// set default pawn class to our Blueprinted character
	static ConstructorHelpers::FClassFinder<APawn> PlayerPawnClassFinder(TEXT("/Game/FirstPersonCPP/Blueprints/FirstPersonCharacter"));
	DefaultPawnClass = PlayerPawnClassFinder.Class;

	// use our custom HUD class
	HUDClass = AInventoryTestHUD::StaticClass();
}
