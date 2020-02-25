// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Components/ActorComponent.h"
#include "Griddable.h"
#include "GridContainer.generated.h"

UENUM(meta=(Bitflags))
enum class EGridContainerType
{
	OTHER,
	HELMET,
	VEST,
	PANTS,
	BACKPACK,
	SHIRT,
	SHOES
};

enum class EInsertOutcome
{
	SUCCESS,
	DOESNT_FIT,
	ALREADY_PRESENT
};

UCLASS(ClassGroup=(Custom), meta=(BlueprintSpawnableComponent))
class INVENTORYTEST_API UGridContainer: public UActorComponent
{
	GENERATED_BODY()

	private:
		// TODO: Should the pointers be to const?
		TArray<TArray<IGriddable*>> m_grid;
		TMap<IGriddable*, TTuple<int32, int32>> m_items;

		bool ItemFits(const IGriddable& item, int32 row, int32 col) const;
		void ZeroOutItem(const IGriddable& item, int32 row, int32 col);

	protected:
		// Called when the game starts
		virtual void BeginPlay() override;

	public:
		// Sets default values for this component's properties
		UGridContainer();

		virtual void PostInitProperties() override;

		UPROPERTY(EditInstanceOnly)
		EGridContainerType Type;

		UPROPERTY(EditInstanceOnly)
		int32 NumRows;

		UPROPERTY(EditInstanceOnly)
		int32 NumColumns;

		int32 Num() const;

		// Called every frame
		virtual void TickComponent(
			float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction) override;

		EInsertOutcome Insert(IGriddable& item, int32 row, int32 col);

		bool Remove(IGriddable& item);

		IGriddable* operator()(int32 row, int32 col) const;
};
